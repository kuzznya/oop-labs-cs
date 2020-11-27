using System.Collections.Generic;
using System.Linq;
using Backups.Models.ObjectDelta;

namespace Backups.Models
{
    public class ObjectDiff
    {
        public BackupFile BaseFile { get; }
        
        public List<IObjectDelta> Changes { get; }

        public ObjectDiff(BackupFile baseFile, List<IObjectDelta> changes)
        {
            BaseFile = baseFile;
            Changes = changes;
        }

        public BackupFile Apply()
        {
            return Changes.Aggregate(BaseFile,
                (current, delta) => delta.Apply(current));
        }
    }
}