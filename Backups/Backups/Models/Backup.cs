using System.Collections.Generic;
using Backups.Cleaners;
using Backups.Models.RestorePoint;
using Backups.StorageManagers;

namespace Backups.Models
{
    public class Backup
    {
        private IStorageManager _storageManager;
        private ICleaner _cleaner;

        IReadOnlyList<IRestorePoint> RestorePoints { get; }

        public Backup(IStorageManager storageManager, ICleaner cleaner)
        {
            _storageManager = storageManager;
            _cleaner = cleaner;
        }

        public void CreateRestorePoint()
        {
            
        }
        
    }
}