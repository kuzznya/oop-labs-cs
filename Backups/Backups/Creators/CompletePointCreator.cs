using System;
using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class CompletePointCreator : IPointCreator
    {
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            var objects = backup.FilePaths
                .Select(path => new BackupObject(path, File.ReadAllBytes(path)))
                .ToList();

            return new CompleteRestorePoint(currentTime, objects);
        }
    }
}