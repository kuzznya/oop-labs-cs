using System.Collections.Generic;
using System.Linq;
using Backups.Models.ObjectDelta;

namespace Backups.Models
{
    public class ObjectDiff
    {
        public BackupObject BaseObject { get; }
        
        public List<IObjectDelta> Changes { get; }

        public ObjectDiff(BackupObject baseObject, List<IObjectDelta> changes)
        {
            BaseObject = baseObject;
            Changes = changes;
        }

        public BackupObject Apply()
        {
            return Changes.Aggregate(BaseObject,
                (current, delta) => delta.Apply(current));
        }
    }
}