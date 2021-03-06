using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Models.RestorePoint
{
    public class CompleteRestorePoint : IRestorePoint
    {
        public DateTime CreationTime { get; }
        public IReadOnlyList<BackupFile> SavedFiles { get; }
        public int Size => SavedFiles.Sum(obj => obj.Data.Length);
        public void Restore()
        {
            foreach (var backupObject in SavedFiles) 
                backupObject.Restore();
        }

        public CompleteRestorePoint(DateTime creationTime, IReadOnlyList<BackupFile> objects)
        {
            CreationTime = creationTime;
            SavedFiles = objects;
        }
    }
}