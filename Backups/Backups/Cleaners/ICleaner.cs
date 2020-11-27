using System.Collections.Generic;
using Backups.Models.RestorePoint;

namespace Backups.Cleaners
{
    public interface ICleaner
    {
        List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points);
    }
}