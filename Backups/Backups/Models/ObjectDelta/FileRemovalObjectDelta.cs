namespace Backups.Models.ObjectDelta
{
    public class FileRemovalObjectDelta : IObjectDelta
    {
        public int Size => 0;

        public BackupFile Apply(BackupFile obj)
        {
            return new BackupFile(obj.Path, new byte[0]);
        }
    }
}