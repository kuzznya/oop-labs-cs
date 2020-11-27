using System.Linq;
using System.Text;
using Backups.Cleaners;
using Backups.Creators;
using Backups.Models;
using Backups.Models.RestorePoint;
using Backups.Storage;
using Backups.Utils;
using NUnit.Framework;

namespace Backups.Tests
{
    [TestFixture]
    public class BackupTest
    {
        [Test]
        public void Backup_WhenCompletePointCreator_CreateComplete()
        {
            var backupRepository = new PlainBackupRepository();
            var creator = new CompletePointCreator(new MockFileReader());
            var cleaner = new CountCleaner(5);
            var dateTimeProvider = new DateTimeProvider();
            
            var backup = new Backup(backupRepository, creator, cleaner, dateTimeProvider, 
                new []{"file1", "file2"});
            
            backup.CreateRestorePoint();

            Assert.That(backup.RestorePoints[0], Is.InstanceOf(typeof(CompleteRestorePoint)));
        }

        [Test]
        public void Backup_WhenIncrementalPointCreator_CreateIncremental()
        {
            var backupRepository = new PlainBackupRepository();
            var fileReader = new MockFileReader();
            var creator = new IncrementalPointCreator(fileReader);
            var cleaner = new CountCleaner(5);
            var dateTimeProvider = new DateTimeProvider();
            
            fileReader.EditFile("file1", "123456789");
            fileReader.EditFile("file2", "aaaaaaaaaaaaa");
            
            var backup = new Backup(backupRepository, creator, cleaner, dateTimeProvider, 
                new []{"file1", "file2"});
            
            backup.CreateRestorePoint();
            
            fileReader.EditFile("file1", "12345");
            
            backup.CreateRestorePoint();

            Assert.That(backup.RestorePoints[0], Is.InstanceOf<CompleteRestorePoint>());
            Assert.That(backup.RestorePoints[0].SavedFiles[0].Data.ToArray(), 
                Is.EqualTo(Encoding.ASCII.GetBytes("123456789")));
            
            Assert.That(backup.RestorePoints[1], Is.InstanceOf<IncrementalRestorePoint>());
            Assert.That(backup.RestorePoints[1].SavedFiles[0].Data.ToArray(), 
                Is.EqualTo(Encoding.ASCII.GetBytes("12345")));
        }

        public void Backup_WhenMixedPointCreator_CreateMixed()
        {
            var backupRepository = new PlainBackupRepository();
            var fileReader = new MockFileReader();
            var creator = new MixedPointCreator(fileReader);
            var cleaner = new CountCleaner(5);
            var dateTimeProvider = new DateTimeProvider();
            
            fileReader.EditFile("file1", "123456789");
            fileReader.EditFile("file2", "aaaaaaaaaaaaa");
            
            var backup = new Backup(backupRepository, creator, cleaner, dateTimeProvider, 
                new []{"file1", "file2"});
            
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();

            Assert.That(backup.RestorePoints[0], Is.InstanceOf<CompleteRestorePoint>());
            Assert.That(backup.RestorePoints[1], Is.InstanceOf<IncrementalRestorePoint>());
            Assert.That(backup.RestorePoints[2], Is.InstanceOf<CompleteRestorePoint>());
        }

        [Test]
        public void Backup_WhenExceedCountLimit_RemoveOldPoints()
        {
            IBackupRepository backupRepository = new PlainBackupRepository();
            MockFileReader fileReader = new MockFileReader();
            IPointCreator creator = new MixedPointCreator(fileReader);
            ICleaner cleaner = new CountCleaner(2);
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            
            fileReader.EditFile("file1", "123456789");
            fileReader.EditFile("file2", "aaaaaaaaaaaaa");
            
            var backup = new Backup(backupRepository, creator, cleaner, dateTimeProvider, 
                new []{"file1", "file2"});
            
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(2));
        }
        
        [Test]
        public void Backup_WhenExceedSizeLimit_RemoveOldPoints()
        {
            IBackupRepository backupRepository = new PlainBackupRepository();
            MockFileReader fileReader = new MockFileReader();
            IPointCreator creator = new CompletePointCreator(fileReader);
            ICleaner cleaner = new SizeCleaner(40);
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            
            fileReader.EditFile("file1", "123456789");
            fileReader.EditFile("file2", "aaaaaaaaaaaaa");
            
            var backup = new Backup(backupRepository, creator, cleaner, dateTimeProvider, 
                new []{"file1", "file2"});
            
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();
            backup.CreateRestorePoint();
            
            Assert.That(backup.RestorePoints.Count, Is.EqualTo(1));
        }
    }
}