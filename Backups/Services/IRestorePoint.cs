using System;
using System.Collections.Generic;

namespace Backups
{
    public abstract class IRestorePoint
    {
        public IRestorePoint(List<string> directoryFiles, int numberRestorePoint, Repository repository, bool localKeep)
        {
            DirectoryFiles = directoryFiles;
            CreateDataTime = DateTime.Now;
            NumberRestorePoint = numberRestorePoint;
            CurrentRepository = repository;
            CurrentLocalKeep = localKeep;
        }

        protected DateTime CreateDataTime
        {
            get;
        }

        protected List<string> DirectoryFiles
        {
            get;
        }

        protected int NumberRestorePoint
        {
            get;
        }

        protected Repository CurrentRepository
        {
            get;
        }

        protected bool CurrentLocalKeep
        {
            get;
        }

        public abstract int AmountStorages();
        public abstract string GetNameFolder();
    }
}