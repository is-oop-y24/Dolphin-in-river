using System;

namespace Backups
{
    public interface IRestorePoint
    {
        int AmountStorages();
        string GetNameFolder();
    }
}