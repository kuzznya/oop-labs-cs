using System.Collections.Generic;
using System.Linq;
using Backups.Models.RestorePoint;

namespace Backups.Algorithms.Cleaners
{
    public class SizeCleaner : ICleaner
    {
        private readonly int _sizeBytes;
        
        public SizeCleaner(int sizeBytes)
        {
            _sizeBytes = sizeBytes;
        }

        public List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points)
        {
            IEnumerable<IRestorePoint> sortedPoints = points.OrderByDescending(point => point.CreationTime);

            var size = 0;
            
            var result = new List<IRestorePoint>();

            foreach (var point in sortedPoints)
            {
                if (size + point.Size <= _sizeBytes) 
                {
                    result.Add(point);
                    size += point.Size;
                }
                else
                    break;
            }

            return result;
        }
    }
}