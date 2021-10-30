using System;
using System.Collections.Generic;
using System.IO;
using Backups.Tools;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            string directory1 = "C:/Users/Иван/Desktop/1.txt";
            string directory2 = "C:/Users/Иван/Desktop/2.txt";
            var listFile = new List<string>
            {
                directory1,
                directory2,
            };

            bool localKeep = true;
            var backupJob = new BackupJob(listFile, "Split storage", "C:/Users/Иван/Desktop/", localKeep);

            backupJob.CreateRestorePoint();

            backupJob.DeleteFileInBackupJob(directory1);

            backupJob.CreateRestorePoint();
            if (backupJob.AmountPoints() != 2 && backupJob.AmountStorage() != 3)
            {
                throw new BackupsException("Error with creating a backup");
            }
        }
    }
}
