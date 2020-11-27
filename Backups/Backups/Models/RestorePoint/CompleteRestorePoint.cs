using System;
using System.Collections.Generic;
using System.Linq;

namespace Backups.Models.RestorePoint
{
    public class CompleteRestorePoint : IRestorePoint
    {
        public DateTime CreationTime { get; }
        public IReadOnlyList<BackupFile> SavedObjects { get; }
        public int Size => SavedObjects.Sum(obj => obj.Data.Length);
        public void Restore()
        {
            foreach (var backupObject in SavedObjects) 
                backupObject.Restore();
        }

        public CompleteRestorePoint(DateTime creationTime, IReadOnlyList<BackupFile> objects)
        {
            CreationTime = creationTime;
            SavedObjects = objects;
        }
    }
}