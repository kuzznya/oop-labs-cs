using System;
using System.Collections.Generic;
using Backups.Cleaners;
using Backups.Creators;
using Backups.Models;
using Backups.Storage;
using Backups.Utils;

namespace Backups
{
    class Program
    {
        static void Main(string[] args)
        {
            IBackupRepository backupRepository = new PlainBackupRepository();
            IPointCreator pointCreator = new MixedPointCreator(new FileReader());
            ICleaner cleaner = new CountCleaner(5);
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            
            var backup = new Backup(backupRepository, 
                pointCreator, cleaner, dateTimeProvider, 
                new List<string> {"/Users/kuzznya/Programs/es-todo.md"});
            
            backup.CreateRestorePoint();

            foreach (var value in backup.RestorePoints[0].SavedFiles[0].Data)
            {
                Console.Write((char) value);
            }
            
            backup.CreateRestorePoint();

            Console.WriteLine(backup.RestorePoints[1]);
            foreach (var value in backup.RestorePoints[1].SavedFiles[0].Data)
            {
                Console.Write((char) value);
            }
        }
    }
}