using System;
using Backups.Models.RestorePoint;

namespace Backups.Storage
{
    public class FileStorage : IStorage
    {
        public void Save(IRestorePoint restorePoint)
        {
            Console.WriteLine($"Restore point {restorePoint} created");
        }

        public void Delete(IRestorePoint restorePoint)
        {
            Console.WriteLine($"Restore point {restorePoint} deleted");
        }
    }
}