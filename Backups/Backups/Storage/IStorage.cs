using Backups.Models.RestorePoint;

namespace Backups.Storage
{
    public interface IStorage
    {
        void Save(IRestorePoint restorePoint);
        void Delete(IRestorePoint restorePoint);
    }
}