using System;
using Backups.Models;

namespace Backups.Storage
{
    public interface IBackupFileRepository
    {
        public Guid Save(BackupFile file);
        public BackupFile Load(Guid id);
        public void Delete(Guid id);
    }
}