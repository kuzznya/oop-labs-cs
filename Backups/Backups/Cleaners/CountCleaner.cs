using System.Collections.Generic;
using System.Linq;
using Backups.Models.RestorePoint;

namespace Backups.Cleaners
{
    public class CountCleaner : ICleaner
    {
        private int _count;
        
        public CountCleaner(int count)
        {
            _count = count;
        }

        public List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points)
        {
            var count = 0;
            return points
                .OrderByDescending(point => point.CreationTime)
                .Where(point => count++ >= _count && !(point is IncrementalRestorePoint))
                .ToList();
        }
    }
}