using System;

namespace Backups.Models.FileDelta
{
    public class EditFileDelta : IFileDelta
    {
        private readonly int _start;
        private readonly int _end;
        private readonly byte[] _edit;
        
        public EditFileDelta(int start, int end, byte[] edit)
        {
            _start = start;
            _end = end;
            _edit = edit;
        }

        public int Size => _edit.Length + 4 * 2;

        public BackupFile Apply(BackupFile obj)
        {
            if (_start >= obj.Data.Length)
                throw new ArgumentException("Backup object data length is less than object delta position");
            
            var data = new byte[Math.Max(_start + _edit.Length, obj.Data.Length)];

            for (var i = 0; i < _start; i++)
                data[i] = obj.Data[i];

            for (var i = _start; i < _edit.Length; i++)
                data[i] = _edit[i - _start];

            for (var i = _end; i < obj.Data.Length; i++)
                data[i] = obj.Data[i];

            return new BackupFile(obj.Path, data);
        }
    }
}