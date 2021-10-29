using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace Backups
{
    public class SplitRestorePoint : IRestorePoint
    {
        private static int _nextId = 0;
        private int _id;
        private DateTime _createDataTime;
        private List<string> _directoryFiles;
        private int _numberRestorePoint;
        private Repository _repository;
        private bool _localKeep;
        private Folder _folder;

        public SplitRestorePoint(List<string> directoryFiles, int numberRestorePoint, Repository repository, bool localKeep)
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
            return _directoryFiles.Count;
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
                dirInfo.CreateSubdirectory(_numberRestorePoint + "_SplitRestorePoint/");
                foreach (string item in _directoryFiles)
                {
                    string archivePath = _repository.GetPath() + _numberRestorePoint + "_SplitRestorePoint/" +
                                         GetName(item) + ".zip";
                    ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
                    archive.CreateEntryFromFile(item, _numberRestorePoint + "_" + GetName(item));
                    archive.Dispose();
                }
            }
            else
            {
                _folder = new Folder(_numberRestorePoint + "_SplitRestorePoint");
                var archives = new List<Archive>();
                foreach (string item in _directoryFiles)
                {
                    var bufferList = new List<string>();
                    bufferList.Add(item);
                    archives.Add(new Archive(GetName(item) + ".zip", bufferList));
                }

                _folder.AddStorage(new SplitBackupsInMemory(archives));
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