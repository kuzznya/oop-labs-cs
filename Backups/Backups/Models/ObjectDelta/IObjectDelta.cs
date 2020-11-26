namespace Backups.Models.ObjectDelta
{
    public interface IObjectDelta
    {
        BackupObject Apply(BackupObject obj);
    }
}