using System;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class MixedPointCreator : IPointCreator
    {
        private readonly CompletePointCreator _completePointCreator;
        private readonly IncrementalPointCreator _incrementalPointCreator;

        public MixedPointCreator(IFileReader reader)
        {
            _completePointCreator = new CompletePointCreator(reader);
            _incrementalPointCreator = new IncrementalPointCreator(reader);
        }
        
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            if (!(backup.RestorePoints.LastOrDefault() is CompleteRestorePoint))
                return _completePointCreator.Create(backup, currentTime);

            return _incrementalPointCreator.Create(backup, currentTime);
        }
    }
}