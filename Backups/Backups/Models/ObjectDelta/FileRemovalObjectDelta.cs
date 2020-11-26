namespace Backups.Models.ObjectDelta
{
    public class FileRemovalObjectDelta : IObjectDelta
    {
        public BackupObject Apply(BackupObject obj)
        {
            return new BackupObject(obj.Path, new byte[0]);
        }
    }
}