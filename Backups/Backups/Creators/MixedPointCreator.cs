using System;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class MixedPointCreator : IPointCreator
    {
        private readonly CompletePointCreator _completePointCreator = new CompletePointCreator();
        private readonly IncrementalPointCreator _incrementalPointCreator = new IncrementalPointCreator();
        
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            if (!(backup.RestorePoints.LastOrDefault() is CompleteRestorePoint))
                return _completePointCreator.Create(backup, currentTime);

            return _incrementalPointCreator.Create(backup, currentTime);
        }
    }
}