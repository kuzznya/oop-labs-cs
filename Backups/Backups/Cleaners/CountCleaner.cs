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

        public List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points) =>
            points
                .OrderByDescending(point => point.CreationTime)
                .TakeLast(_count)
                .ToList();
    }
}