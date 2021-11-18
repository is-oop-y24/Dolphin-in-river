namespace Banks
{
    public interface ITransaction
    {
        void Cancel();
        int GetId();

        string GetTypeTransaction();
        string GetInformation();
    }
}