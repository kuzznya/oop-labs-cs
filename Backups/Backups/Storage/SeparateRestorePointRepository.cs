using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models.RestorePoint;

namespace Backups.Storage
{
    public class SeparateRestorePointRepository : IRestorePointRepository
    {
        private IBackupFileRepository _fileRepository;
        
        private readonly Dictionary<Guid, List<RestorePointMetadata>> _data = new Dictionary<Guid, List<RestorePointMetadata>>();

        public SeparateRestorePointRepository(IBackupFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }


        public List<IRestorePoint> Load(Guid backupId)
        {
            return _data[backupId]
                .Select(metadata => new CompleteRestorePoint(metadata.CreationTime, metadata.FileIds
                    .Select(fileId => _fileRepository.Load(fileId))
                    .ToList()))
                .Select(point => (IRestorePoint) point)
                .ToList();
        }

        public void Save(Guid backupId, IRestorePoint point)
        {
            var fileIds = point.SavedFiles.Select(file => _fileRepository.Save(file)).ToList();
            var metadata = new RestorePointMetadata(point.CreationTime, fileIds);
            if (_data.ContainsKey(backupId))
                _data[backupId].Add(metadata);
            _data[backupId] = new List<RestorePointMetadata> {metadata};
        }

        public void Delete(Guid backupId, IRestorePoint point)
        {
            var pointMetadata = _data[backupId]
                .First(metadata => metadata.CreationTime == point.CreationTime);

            foreach (var id in pointMetadata.FileIds)
                _fileRepository.Delete(id);

            _data.Remove(backupId);
        }

        private class RestorePointMetadata
        {
            public DateTime CreationTime { get; }
            public List<Guid> FileIds { get; }

            public RestorePointMetadata(DateTime creationTime, List<Guid> fileIds)
            {
                CreationTime = creationTime;
                FileIds = fileIds;
            }
        }
    }
}