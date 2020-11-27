using System;
using System.Collections.Generic;

namespace Backups.Models.RestorePoint                        

{
    public interface IRestorePoint
    {
        public DateTime CreationTime { get; }
        public IReadOnlyList<BackupObject> SavedObjects { get; }
        public int Size { get; }
        public void Restore();
    }
}