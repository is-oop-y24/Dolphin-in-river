namespace Backups
{
    public class SingleBackupsInMemory : IBackupsInMemory
    {
        private Archive _archive;

        public SingleBackupsInMemory(Archive archive)
        {
            _archive = archive;
        }

        public int GetAmountStorage()
        {
            return _archive.GetAmount();
        }
    }
}