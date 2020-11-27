using System;
using Backups.Models;
using Backups.Models.RestorePoint;

namespace Backups.Creators
{
    public interface IPointCreator
    {
        public IRestorePoint Create(Backup backup, DateTime currentTime);
    }
}