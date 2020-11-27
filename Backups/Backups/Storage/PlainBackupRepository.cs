using System;
using System.Collections.Generic;
using Backups.Models;

namespace Backups.Storage
{
    public class PlainBackupRepository : IBackupRepository
    {
        private readonly Dictionary<Guid, Backup> _backups = new Dictionary<Guid, Backup>();

        public Backup Load(Guid id) => _backups[id];

        public void Save(Backup backup) => _backups[backup.Id] = backup;
        
        public void Delete(Backup backup) => _backups.Remove(backup.Id);
    }
}