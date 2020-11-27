namespace Backups.Models.ObjectDelta
{
    public interface IObjectDelta
    {
        public int Size { get; }
        BackupObject Apply(BackupObject obj);
    }
}