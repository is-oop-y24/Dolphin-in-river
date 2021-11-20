namespace Banks
{
    public enum TypeTransactions
    {
        AddMoney,
        WithDraw,
        Transfer,
    }

    public interface ITransaction
    {
        void Cancel();
        int GetId();

        TypeTransactions GetTypeTransaction();
        string GetInformation();
    }
}