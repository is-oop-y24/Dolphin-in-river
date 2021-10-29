using System.Collections.Generic;

namespace Backups.Services
{
    public interface IBackupService
    {
        BackupJob CreateBackupJob(List<string> file, string configuration, string path, bool localKeep);
    }
}