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
        private Folder _folder;

        public SplitRestorePoint(List<string> directoryFiles, int numberRestorePoint, Repository repository, bool localKeep)
            : base(directoryFiles, numberRestorePoint, repository, localKeep)
        {
            _id = ++_nextId;
            CreateStorage();
        }

        public override int AmountStorages()
        {
            return DirectoryFiles.Count;
        }

        public override string GetNameFolder()
        {
            return _folder.GetName();
        }

        private void CreateStorage()
        {
            if (CurrentLocalKeep)
            {
                var dirInfo = new DirectoryInfo(CurrentRepository.GetPath());
                dirInfo.CreateSubdirectory(NumberRestorePoint + "_SplitRestorePoint/");
                foreach (string item in DirectoryFiles)
                {
                    string archivePath = CurrentRepository.GetPath() + NumberRestorePoint + "_SplitRestorePoint/" +
                                         GetName(item) + ".zip";
                    ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
                    archive.CreateEntryFromFile(item, NumberRestorePoint + "_" + GetName(item));
                    archive.Dispose();
                }
            }
            else
            {
                _folder = new Folder(NumberRestorePoint + "_SplitRestorePoint");
                var archives = new List<Archive>();
                foreach (string item in DirectoryFiles)
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