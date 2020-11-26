using System;
using System.Collections.Generic;

namespace Backups.Models.RestorePoint
{
    public class CompleteRestorePoint : IRestorePoint
    {
        public DateTime CreationTime { get; }
        public IReadOnlyList<BackupObject> SavedObjects { get; }

        public CompleteRestorePoint(DateTime creationTime, IReadOnlyList<BackupObject> objects)
        {
            CreationTime = creationTime;
            SavedObjects = objects;
        }
    }
}