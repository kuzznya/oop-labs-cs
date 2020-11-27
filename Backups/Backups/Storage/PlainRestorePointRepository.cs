using System;
using System.Collections.Generic;
using Backups.Models.RestorePoint;

namespace Backups.Storage
{
    public class PlainRestorePointRepository : IRestorePointRepository
    {
        private readonly Dictionary<Guid, List<IRestorePoint>> _data = new Dictionary<Guid, List<IRestorePoint>>();

        public List<IRestorePoint> Load(Guid backupId) => _data[backupId];

        public void Save(Guid backupId, IRestorePoint point)
        {
            if (_data.ContainsKey(backupId))
                _data[backupId].Add(point);
            _data[backupId] = new List<IRestorePoint> {point};
        }

        public void Delete(Guid backupId, IRestorePoint point) => _data[backupId].Remove(point);
    }
}