using System;
using System.IO;
using Backups;
using BackupsExtra.Logging;

namespace BackupsExtra
{
    public class RestorePointWithDateCreation
    {
        public RestorePointWithDateCreation(IRestorePoint point, AbstractLogging logging)
        {
            Point = point;
            Logging = logging;
            NewRepository = new RepositoryExtra(logging);
        }

        public IRestorePoint Point
        {
            get;
            set;
        }

        public AbstractLogging Logging
        {
            get;
            set;
        }

        public RepositoryExtra NewRepository
        {
            get;
            set;
        }

        public void RecoverSplitPoint(IRecovery recoveryInfo)
        {
            NewRepository.RecoverSplitPoint(recoveryInfo, Point);
        }

        public void RecoverSinglePoint(IRecovery recoveryInfo)
        {
            NewRepository.RecoverSinglePoint(recoveryInfo, Point);
        }

        public void ExecuteLoggingCreationPoint()
        {
            string text = null;
            if (Logging.Configuration)
            {
                text += Point.CreateDataTime + ", ";
            }

            text += Point.GetType() + " has been created" + Environment.NewLine;
            if (Logging.TypeLogging.Equals(TypeLogging.ConsoleLogging))
            {
                Console.WriteLine(text);
            }
            else
            {
                var loggingToFile = (FileLogging)Logging;
                var fileStream = new FileStream(loggingToFile.FilePath, FileMode.Append);
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                fileStream.Write(array, 0, array.Length);
                fileStream.Dispose();
            }
        }
    }
}