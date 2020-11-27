using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models.RestorePoint;
using Backups.Utils;

namespace Backups.Cleaners
{
    public class DateCleaner : ICleaner
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly TimeSpan _span;
        
        public DateCleaner(IDateTimeProvider dateTimeProvider, TimeSpan span)
        {
            _dateTimeProvider = dateTimeProvider;
            _span = span;
        }

        public List<IRestorePoint> FilterRestorePoints(IEnumerable<IRestorePoint> points) =>
            points
                .Where(point => point.CreationTime > _dateTimeProvider.GetCurrentDateTime().Subtract(_span))
                .ToList();
    }
}