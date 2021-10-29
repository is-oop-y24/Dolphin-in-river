using System.Collections.Generic;

namespace Backups
{
    public class SplitBackupsInMemory : IBackupsInMemory
    {
        private List<Archive> _archives;

        public SplitBackupsInMemory(List<Archive> archives)
        {
            _archives = archives;
        }

        public int GetAmountStorage()
        {
            int result = 0;
            foreach (var item in _archives)
            {
                result += item.GetAmount();
            }

            return result;
        }
    }
}