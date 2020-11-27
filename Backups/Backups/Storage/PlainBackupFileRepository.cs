using System;
using System.Collections.Generic;
using Backups.Models;

namespace Backups.Storage
{
    public class PlainBackupFileRepository : IBackupFileRepository
    {
        private readonly Dictionary<Guid, BackupFile> _data = new Dictionary<Guid, BackupFile>();
        
        public Guid Save(BackupFile file)
        {
            var id = Guid.NewGuid();
            _data[id] = file;
            return id;
        }

        public BackupFile Load(Guid id) => _data[id];

        public void Delete(Guid id) => _data.Remove(id);
    }
}