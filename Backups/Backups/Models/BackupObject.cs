using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Backups.Models.ObjectDelta;

namespace Backups.Models
{
    public class BackupObject
    {
        public string Path { get; }
        public ImmutableArray<byte> Data { get; }
        
        public bool IsDeleted => Data.IsEmpty;

        public BackupObject(string path, IEnumerable<byte> data)
        {
            Path = path;
            Data = data.ToImmutableArray();
        }

        public ObjectDiff CompareTo(BackupObject previousObject)
        {
            if (Data.IsEmpty && !previousObject.Data.IsEmpty)
                return new ObjectDiff(previousObject, 
                    new List<IObjectDelta> {new FileRemovalObjectDelta()});

            var deltas = new List<IObjectDelta>();
            
            var obj = previousObject;
            while (true)
            {
                var delta = FindClosestDelta(obj);
                if (delta is null)
                    break;
                deltas.Add(delta);
                obj = delta.Apply(obj);
            }
            
            return new ObjectDiff(previousObject, deltas);
        }

        private IObjectDelta FindClosestDelta(BackupObject previousObject)
        {
            if (Data.IsEmpty && !previousObject.Data.IsEmpty)
                return new FileRemovalObjectDelta();

            if (Data.SequenceEqual(previousObject.Data))
                return null;

            var deltaCurrentStart = 0;
            var deltaPreviousStart = 0;
            var deltaCurrentEnd = 0;
            var deltaPreviousEnd = 0;

            while (deltaCurrentStart < Data.Length &&
                   deltaPreviousStart < previousObject.Data.Length &&
                   Data[deltaCurrentStart] == previousObject.Data[deltaPreviousStart])
            {
                deltaCurrentStart++;
                deltaPreviousStart++;
            }
            
            if (deltaCurrentStart == Data.Length)
                return new DeletionObjectDelta(deltaCurrentStart, 
                    previousObject.Data.Length - deltaCurrentStart);
            
            if (deltaPreviousStart == previousObject.Data.Length)
                return new AdditionObjectDelta(deltaPreviousStart, 
                    Data.TakeLast(Data.Length - deltaPreviousStart).ToArray());

            while (deltaCurrentEnd < Data.Length &&
                   deltaPreviousEnd < previousObject.Data.Length &&
                   Data[deltaCurrentEnd] != previousObject.Data[deltaPreviousEnd])
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