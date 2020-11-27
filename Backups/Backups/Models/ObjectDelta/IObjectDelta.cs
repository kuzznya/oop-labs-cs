namespace Backups.Models.ObjectDelta
{
    public interface IObjectDelta
    {
        public int Size { get; }
        BackupFile Apply(BackupFile obj);
    }
}