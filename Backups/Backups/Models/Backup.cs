using System.Collections.Generic;
using System.Linq;
using Backups.Cleaners;
using Backups.Creators;
using Backups.Models.RestorePoint;
using Backups.Utils;

namespace Backups.Models
{
    public class Backup
    {
        private readonly IPointCreator _creator;
        private readonly ICleaner _cleaner;
        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly List<string> _filePaths;
        public IReadOnlyList<string> FilePaths => _filePaths;

        private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();
        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints;

        public Backup(
            IPointCreator creator, 
            ICleaner cleaner, 
            IDateTimeProvider dateTimeProvider, 
            IEnumerable<string> filePaths)
        {
            _creator = creator;
            _cleaner = cleaner;
            _dateTimeProvider = dateTimeProvider;
            _filePaths = filePaths.ToList();
        }

        public void AddFile(string path) => 
            _filePaths.Add(path);

        public void RemoveFile(string path) =>
            _filePaths.Remove(path);

        public void CreateRestorePoint()
        {
            _restorePoints.Add(_creator.Create(this, _dateTimeProvider.GetCurrentDateTime()));

            _restorePoints = _cleaner.FilterRestorePoints(_restorePoints);
        }

        public void RestoreLast()
        {
            RestorePoints.LastOrDefault()?.Restore();
        }
        
    }
}