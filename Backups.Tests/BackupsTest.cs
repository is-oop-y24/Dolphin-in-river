using System.Collections.Generic;
using Backups.Services;
using NUnit.Framework;
namespace Backups.Tests
{
    public class BackupsTest
    {
        private IBackupService _backupService;
        [SetUp]
        public void Setup()
        {
            _backupService = new BackupService();
        }
        
        
        [Test]
        public void CheckCreateRestorePointsAndStorages_OnFileSystem()
        {
            Setup();
            string directory1 = "C:/Users/Иван/Desktop/1.txt";
            string directory2 = "C:/Users/Иван/Desktop/2.txt";
            var listFile = new List<string>
            {
                directory1,
                directory2,
            };

            bool localKeep = false;
            var backupJob = new BackupJob(listFile, "Single storage", "LocalKeep", localKeep);
            
            backupJob.CreateRestorePoint();

            backupJob.DeleteFile(directory1);

            backupJob.CreateRestorePoint();

            Assert.AreEqual(2, backupJob.AmountPoints());
            Assert.AreEqual(3, backupJob.AmountStorage());
        }
    }
}