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

        public Repository CurrentRepository
        {
            get;
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

        protected bool CurrentLocalKeep
        {
            get;
        }
    }
}