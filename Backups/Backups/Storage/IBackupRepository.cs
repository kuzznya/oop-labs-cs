using System;
using Backups.Models;

namespace Backups.Storage
{
    public interface IBackupRepository
    {
        public Backup Load(Guid id);
        public void Save(Backup backup);
        public void Delete(Backup backup);
    }
}