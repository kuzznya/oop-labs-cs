using System.IO;
using Backups.Models;

namespace Backups.Creators
{
    public class FileReader : IFileReader
    {
        public BackupFile Read(string path)
        {
            return new BackupFile(path, File.ReadAllBytes(path));
        }
    }
}