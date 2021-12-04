namespace BackupsExtra.Logging
{
    public class FileLogging : AbstractLogging
    {
        public FileLogging(string filePath)
        {
            FilePath = filePath;
            TypeLogging = TypeLogging.FileLogging;
        }

        public string FilePath
        {
            get;
        }
    }
}