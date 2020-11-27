using System.Collections.Generic;
using System.Linq;
using Backups.Models.ObjectDelta;

namespace Backups.Models
{
    public class FileDiff
    {
        public BackupFile BaseFile { get; }
        
        public List<IFileDelta> Changes { get; }

        public FileDiff(BackupFile baseFile, List<IFileDelta> changes)
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