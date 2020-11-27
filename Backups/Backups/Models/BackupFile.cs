using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Backups.Models.ObjectDelta;

namespace Backups.Models
{
    public class BackupFile
    {
        public string Path { get; }
        public ImmutableArray<byte> Data { get; }
        
        public bool IsDeleted => Data.IsEmpty;

        public BackupFile(string path, IEnumerable<byte> data)
        {
            Path = path;
            Data = data.ToImmutableArray();
        }

        public void Restore()
        {
            File.WriteAllBytes(Path, Data.ToArray());
        }

        public ObjectDiff CompareTo(BackupFile previousFile)
        {
            if (Data.IsEmpty && !previousFile.Data.IsEmpty)
                return new ObjectDiff(previousFile, 
                    new List<IObjectDelta> {new FileRemovalObjectDelta()});

            var deltas = new List<IObjectDelta>();
            
            var obj = previousFile;
            while (true)
            {
                var delta = FindClosestDelta(obj);
                if (delta is null)
                    break;
                deltas.Add(delta);
                obj = delta.Apply(obj);
            }
            
            return new ObjectDiff(previousFile, deltas);
        }

        private IObjectDelta FindClosestDelta(BackupFile previousFile)
        {
            if (Data.IsEmpty && !previousFile.Data.IsEmpty)
                return new FileRemovalObjectDelta();

            if (Data.SequenceEqual(previousFile.Data))
                return null;

            var deltaCurrentStart = 0;
            var deltaPreviousStart = 0;
            var deltaCurrentEnd = 0;
            var deltaPreviousEnd = 0;

            while (deltaCurrentStart < Data.Length &&
                   deltaPreviousStart < previousFile.Data.Length &&
                   Data[deltaCurrentStart] == previousFile.Data[deltaPreviousStart])
            {
                deltaCurrentStart++;
                deltaPreviousStart++;
            }
            
            if (deltaCurrentStart == Data.Length)
                return new DeletionObjectDelta(deltaCurrentStart, 
                    previousFile.Data.Length - deltaCurrentStart);
            
            if (deltaPreviousStart == previousFile.Data.Length)
                return new AdditionObjectDelta(deltaPreviousStart, 
                    Data.TakeLast(Data.Length - deltaPreviousStart).ToArray());

            while (deltaCurrentEnd < Data.Length &&
                   deltaPreviousEnd < previousFile.Data.Length &&
                   Data[deltaCurrentEnd] != previousFile.Data[deltaPreviousEnd])
            {
                deltaCurrentEnd++;
                deltaPreviousEnd++;
            }

            return new EditObjectDelta(
                deltaPreviousStart,
                deltaPreviousEnd,
                Data
                    .RemoveRange(0, deltaCurrentStart)
                    .RemoveRange(deltaCurrentEnd - deltaCurrentStart, Data.Length - deltaCurrentStart)
                    .ToArray()
            );
        }
    }
}