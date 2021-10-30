using System;
using System.Collections.Generic;
using System.Diagnostics;
using Backups.Services;
using Backups.Tools;

namespace Backups
{
    public class BackupJob
    {
        private List<IRestorePoint> _points = new List<IRestorePoint>();
        private List<string> _files;
        private string _configuration;
        private Repository _repository = null;
        private bool _localKeep;

        public BackupJob(List<string> file, string configuration, string path, bool localKeep)
        {
            _files = file;
            _configuration = configuration;
            _localKeep = localKeep;
            _repository = new Repository(path);
        }

        public IRestorePoint CreateRestorePoint()
        {
            if (_repository == null)
            {
                throw new BackupsException("Not directory storage");
            }

            var files = new List<string>(_files);
            ICreateRestorePoint myPoint;
            if (_configuration == "Split storage")
            {
                myPoint = new CreateSplitRestorePoint();
            }
            else if (_configuration == "Single storage")
            {
                myPoint = new CreateSingleRestorePoint();
            }
            else
            {
                throw new BackupsException("Incorrect storage format");
            }

            var resultPoint = myPoint.CreateRestorePoint(files, _points.Count + 1, _repository, _localKeep);
            _points.Add(resultPoint);
            return resultPoint;
        }

        public void DeleteFileInBackupJob(string directoryFile)
        {
            _files.Remove(directoryFile);
        }

        public int AmountPoints()
        {
            return _points.Count;
        }

        public int AmountStorage()
        {
            int amountStorage = 0;
            foreach (var item in _points)
            {
                amountStorage += item.AmountStorages();
            }

            return amountStorage;
        }

        public string GetNameFolder(IRestorePoint restorePoint)
        {
            return restorePoint.GetNameFolder();
        }
    }
}