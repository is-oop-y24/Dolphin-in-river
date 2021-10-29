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
        private Folder _folder;

        public SingleRestorePoint(List<string> directoryFiles, int numberRestorePoint, Repository repository, bool localKeep)
            : base(directoryFiles, numberRestorePoint, repository, localKeep)
        {
            _id = ++_nextId;
            CreateStorage();
        }

        public override int AmountStorages()
        {
            if (CurrentLocalKeep)
            {
                return DirectoryFiles.Count;
            }
            else
            {
                return _folder.GetCount();
            }
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
                dirInfo.CreateSubdirectory(NumberRestorePoint + "_SingleRestorePoint/");
                string archivePath =
                    CurrentRepository.GetPath() + NumberRestorePoint + "_SingleRestorePoint/" + _id + ".zip";
                ZipArchive archive = ZipFile.Open(archivePath, ZipArchiveMode.Create);
                foreach (string item in DirectoryFiles)
                {
                    archive.CreateEntryFromFile(item, NumberRestorePoint + "_" + GetName(item));
                }

                archive.Dispose();
            }
            else
            {
                _folder = new Folder(NumberRestorePoint + "_SingleRestorePoint");
                var archive = new Archive(_id + ".zip", DirectoryFiles);
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