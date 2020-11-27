namespace Backups.Models.FileDelta
{
    public class FileRemovalFileDelta : IFileDelta
    {
        public int Size => 0;

        public BackupFile Apply(BackupFile obj)
        {
            return new BackupFile(obj.Path, new byte[0]);
        }
    }
}