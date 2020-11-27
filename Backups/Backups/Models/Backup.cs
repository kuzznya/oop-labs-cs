using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Cleaners;
using Backups.Creators;
using Backups.Models.RestorePoint;
using Backups.Storage;
using Backups.Utils;

namespace Backups.Models
{
    public class Backup
    {
        private readonly IBackupRepository _repository;
        private readonly IPointCreator _creator;
        private readonly ICleaner _cleaner;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly List<string> _filePaths;
        public IReadOnlyList<string> FilePaths => _filePaths;

        private List<IRestorePoint> _restorePoints;
        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints
            .OrderBy(point => point.CreationTime)
            .ToImmutableList();

        public Guid Id { get; }

        public Backup(
            IBackupRepository repository,
            IPointCreator creator, 
            ICleaner cleaner, 
            IDateTimeProvider dateTimeProvider, 
            IEnumerable<string> filePaths
            ) : this(Guid.NewGuid(), repository, creator, cleaner, dateTimeProvider, filePaths, new List<IRestorePoint>()) 
        { }

        public Backup(
            Guid id,
            IBackupRepository repository,
            IPointCreator creator,
            ICleaner cleaner,
            IDateTimeProvider dateTimeProvider,
            IEnumerable<string> filePaths,
            IEnumerable<IRestorePoint> restorePoints)
        {
            Id = id;
            _repository = repository;
            _creator = creator;
            _cleaner = cleaner;
            _dateTimeProvider = dateTimeProvider;
            _filePaths = filePaths.ToList();
            _restorePoints = restorePoints.ToList();
        }

        public void AddFile(string path) => 
            _filePaths.Add(path);

        public void RemoveFile(string path) =>
            _filePaths.Remove(path);

        public void CreateRestorePoint()
        {
            _restorePoints.Add(_creator.Create(this, _dateTimeProvider.GetCurrentDateTime()));

            _restorePoints = _cleaner.FilterRestorePoints(_restorePoints);
            
            _repository.Save(this);
        }

        public void RestoreLast()
        {
            RestorePoints.LastOrDefault()?.Restore();
        }
        
    }
}