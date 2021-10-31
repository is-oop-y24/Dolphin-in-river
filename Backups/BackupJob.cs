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
        private ICreateRestorePoint _point;
        private Repository _repository = null;
        private bool _localKeep;

        public BackupJob(List<string> file, ICreateRestorePoint point, string path, bool localKeep)
        {
            _files = file;
            _point = point;
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
            IRestorePoint resultPoint = _point.CreateRestorePoint(files, _points.Count + 1, _repository, _localKeep);
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
            return _repository.GetCount();
        }
    }
}