namespace Backups.Models.FileDelta
{
    public interface IFileDelta
    {
        public int Size { get; }
        BackupFile Apply(BackupFile obj);
    }
}