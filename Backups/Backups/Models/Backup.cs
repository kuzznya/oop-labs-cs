using System.Collections.Generic;
using Backups.Algorithms.Cleaners;
using Backups.Algorithms.StorageManagers;

namespace Backups.Models
{
    public class Backup
    {
        private IStorageManager _storageManager;
        private ICleaner _cleaner;

        IReadOnlyList<RestorePoint.IRestorePoint> RestorePoints { get; }

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