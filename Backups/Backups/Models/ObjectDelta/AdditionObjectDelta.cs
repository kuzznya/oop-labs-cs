using System;

namespace Backups.Models.ObjectDelta
{
    public class AdditionObjectDelta : IObjectDelta
    {
        private readonly int _position;
        private readonly byte[] _addition;
        
        public AdditionObjectDelta(int position, byte[] addition)
        {
            _position = position;
            _addition = addition;
        }

        public int Size => _addition.Length + 4;

        public BackupObject Apply(BackupObject obj)
        {
            var data = new byte[obj.Data.Length + _addition.Length];

            for (var i = 0; i < _position; i++)
                data[i] = obj.Data[i];

            int pos = Math.Min(_position, obj.Data.Length);
            for (var i = pos; i < _addition.Length; i++)
                data[i] = _addition[i - pos];

            for (var i = _position + _addition.Length; i < data.Length; i++)
                data[i] = obj.Data[i - _addition.Length];
            
            return new BackupObject(obj.Path, data);
        }
    }
}