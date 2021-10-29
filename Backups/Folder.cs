using System.Collections.Generic;

namespace Backups
{
    public class Folder
    {
        private string _nameFolder;

        private IBackupsInMemory _storage;
        public Folder(string nameFolder)
        {
            _nameFolder = nameFolder;
        }

        public void AddStorage(IBackupsInMemory storage)
        {
            _storage = storage;
        }

        public int GetCount()
        {
            return _storage.GetAmountStorage();
        }

        public string GetName()
        {
            return _nameFolder;
        }
    }
}