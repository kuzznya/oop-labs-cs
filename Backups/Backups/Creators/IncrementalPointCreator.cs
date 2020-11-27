using System;
using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class IncrementalPointCreator : IPointCreator
    {
        private readonly CompletePointCreator _completePointCreator = new CompletePointCreator();
        
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            if (backup.RestorePoints.Count == 0)
                return _completePointCreator.Create(backup, currentTime);

            var objects = backup.FilePaths
                .Select(path => new BackupObject(path, File.ReadAllBytes(path)))
                .ToList();

            return new IncrementalRestorePoint(backup.RestorePoints.Last(), objects, currentTime);
        }
    }
}