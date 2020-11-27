using System;
using System.Linq;

namespace Backups.Models.ObjectDelta
{
    public class DeletionObjectDelta : IObjectDelta
    {
        private readonly int _position;
        private readonly int _length;
        
        public DeletionObjectDelta(int position, int length)
        {
            _position = position;
            _length = length;
        }

        public int Size => 4 * 2;

        public BackupFile Apply(BackupFile obj)
        {
            if (_position > obj.Data.Length)
                return new BackupFile(obj.Path, obj.Data.ToArray());

            var data = obj.Data
                .RemoveRange(_position, Math.Min(_length, obj.Data.Length - _position));
            
            return new BackupFile(obj.Path, data);
        }
    }
}