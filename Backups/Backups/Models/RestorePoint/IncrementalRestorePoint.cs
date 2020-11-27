using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Backups.Models.RestorePoint
{
    public class IncrementalRestorePoint : IRestorePoint
    {
        private readonly IRestorePoint _previousPoint;
        private readonly List<ObjectDiff> _diffs = new List<ObjectDiff>();
        
        public DateTime CreationTime { get; }

        public IReadOnlyList<BackupObject> SavedObjects => _previousPoint.SavedObjects
            .Where(obj => _diffs.Find(diff => diff.BaseObject == obj) is null)
            .Concat(_diffs.Select(diff => diff.Apply()))
            .ToImmutableList();

        public int Size => _diffs.Sum(diff => diff.Changes.Sum(delta => delta.Size));

        public IncrementalRestorePoint(
            IRestorePoint previousPoint, 
            IEnumerable<BackupObject> objects, 
            DateTime creationTime)
        {
            _previousPoint = previousPoint;
            CreationTime = creationTime;

            var previousObjects = previousPoint.SavedObjects;
            foreach (var obj in objects)
            {
                var prevObj = previousObjects.FirstOrDefault(o => o.Path == obj.Path);
                _diffs.Add(prevObj != null
                    ? obj.CompareTo(prevObj)
                    : obj.CompareTo(new BackupObject(obj.Path, new byte[0])));
            }
        }
    }
}