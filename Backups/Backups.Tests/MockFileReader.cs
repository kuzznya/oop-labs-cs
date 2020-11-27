using System.Collections.Generic;
using System.Text;
using Backups.Creators;
using Backups.Models;

namespace Backups.Tests
{
    public class MockFileReader : IFileReader
    {
        private Dictionary<string, byte[]> _files = new Dictionary<string, byte[]>();

        public void EditFile(string path, string content)
        {
            _files[path] = Encoding.ASCII.GetBytes(content);
        }

        public void RemoveFile(string path)
        {
            _files.Remove(path);
        }
        
        public BackupFile Read(string path)
        {
            return new BackupFile(path, _files.ContainsKey(path) ? _files[path] : new byte[0]);
        }
    }
}