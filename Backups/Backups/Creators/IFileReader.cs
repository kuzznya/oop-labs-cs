using Backups.Models;

namespace Backups.Creators
{
    public interface IFileReader
    {
        BackupFile Read(string path);
    }
}