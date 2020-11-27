using System;
using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class IncrementalPointCreator : IPointCreator
    {
        private readonly CompletePointCreator _completePointCreator;

        private readonly IFileReader _reader;

        public IncrementalPointCreator(IFileReader reader)
        {
            _reader = reader;
            _completePointCreator = new CompletePointCreator(reader);
        }
        
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            if (backup.RestorePoints.Count == 0)
                return _completePointCreator.Create(backup, currentTime);

            var objects = backup.FilePaths
                .Select(path => _reader.Read(path))
                .ToList();

            return new IncrementalRestorePoint(backup.RestorePoints.Last(), objects, currentTime);
        }
    }
}