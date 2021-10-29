using System.Collections.Generic;
using Backups.Services;

namespace Backups
{
    public class BackupService : IBackupService
    {
        private List<BackupJob> _backups = new List<BackupJob>();

        public BackupService()
        {
        }

        public BackupJob CreateBackupJob(List<string> file, string configuration, string path, bool localKeep)
        {
            var backup = new BackupJob(file, configuration, path, localKeep);
            _backups.Add(backup);
            return backup;
        }
    }
}