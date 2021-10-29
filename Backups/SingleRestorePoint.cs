using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups
{
    public class SingleRestorePoint : IRestorePoint
    {
        private static int _nextId = 0;
        private int _id;
        private DateTime _createDataTime;
        private List<string> _directoryFiles;
        private int _numberRestorePoint;
        private Repository _repository;
        private bool _localKeep;
        private Folder _folder;

        public SingleRestorePoint(List<string> directoryFiles, int numberRestorePoint, Repository repository, bool localKeep)
        {
            _id = ++_nextId;
            _createDataTime = DateTime.Now;
            _directoryFiles = directoryFiles;
            _numberRestorePoint = numberRestorePoint;
            _repository = repository;
            _localKeep = localKeep;
            CreateStorage();
        }

        public int AmountStorages()
        {
            if (_localKeep)
            {
                return _directoryFiles.Count;
            }
            else
            {
                return _folder.GetCount();
            }
        }

        public string GetNameFolder()
        {
            return _folder.GetName();
        }

        private void CreateStorage()
        {
            if (_localKeep)
            {
                var dirInfo = new DirectoryInfo(_repository.GetPath());
                dirInfo.CreateSubdirectory(_numberRestorePoint + "_SingleRestorePoint/");
                string archivePath =
                    _repository.GetPath() + _numberRestorePoint + "_SingleRestorePoint/" + _id + ".zip";
                ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
                foreach (string item in _directoryFiles)
                {
                    archive.CreateEntryFromFile(item, _numberRestorePoint + "_" + GetName(item));
                }

                archive.Dispose();
            }
            else
            {
                _folder = new Folder(_numberRestorePoint + "_SingleRestorePoint");
                var archive = new Archive(_id + ".zip", _directoryFiles);
                var backups = new SingleBackupsInMemory(archive);
                _folder.AddStorage(backups);
            }
        }

        private string GetName(string directoryName)
        {
            string s = null;
            for (int i = directoryName.LastIndexOf("/") + 1; i < directoryName.Length; i++)
            {
                s += directoryName[i];
            }

            return s;
        }
    }
}