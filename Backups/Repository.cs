namespace Backups
{
    public class Repository
    {
        private string _path;

        public Repository(string path)
        {
            _path = path;
        }

        public string GetPath()
        {
            return _path;
        }
    }
}