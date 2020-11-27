using System.Collections.Generic;
using System.Linq;
using Backups.Models.RestorePoint;

namespace Backups.Cleaners
{
    public class HybridCleaner : ICleaner
    {
        private readonly IEnumerable<ICleaner> _cleaners;
        private readonly Mode _mode;
        
        public HybridCleaner(IEnumerable<ICleaner> cleaners, Mode mode)
        {
            _cleaners = cleaners;
            _mode = mode;
        }
        
        public List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points)
        {
            if (_mode == Mode.Soft)
                return _cleaners.Select(cleaner => cleaner.FilterRestorePoints(points))
                    .OrderByDescending(list => list.Count)
                    .Last();
            else
                return _cleaners.Select(cleaner => cleaner.FilterRestorePoints(points))
                    .OrderByDescending(list => list.Count)
                    .First();
        }
        
        public enum Mode
        {
            Scrict,
            Soft
        }
    }
}