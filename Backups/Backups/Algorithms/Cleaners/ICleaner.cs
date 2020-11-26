using System.Collections.Generic;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Algorithms.Cleaners
{
    public interface ICleaner
    {
        List<IRestorePoint> FilterRestorePoints(List<IRestorePoint> points);
    }
}