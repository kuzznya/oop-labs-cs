using System;
using System.Collections.Generic;
using Backups.Models.RestorePoint;

namespace Backups.Storage
{
    public interface IRestorePointRepository
    {
        public List<IRestorePoint> Load(Guid backupId);
        public void Save(Guid backupId, IRestorePoint point);
        public void Delete(Guid backupId, IRestorePoint point);
    }
}