namespace BackupsExtra.Logging
{
    public class FileLogging : AbstractClassLogging
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