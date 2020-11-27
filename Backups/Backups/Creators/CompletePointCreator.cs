using System;
using System.IO;
using System.Linq;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public class CompletePointCreator : IPointCreator
    {
        private readonly IFileReader _reader;

        public CompletePointCreator(IFileReader reader)
        {
            _reader = reader;
        }
        
        public IRestorePoint Create(Backup backup, DateTime currentTime)
        {
            var objects = backup.FilePaths
                .Select(path => _reader.Read(path))
                .ToList();

            return new CompleteRestorePoint(currentTime, objects);
        }
    }
}