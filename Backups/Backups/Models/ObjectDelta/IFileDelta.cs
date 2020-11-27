namespace Backups.Models.ObjectDelta
{
    public interface IFileDelta
    {
        public int Size { get; }
        BackupFile Apply(BackupFile obj);
    }
}