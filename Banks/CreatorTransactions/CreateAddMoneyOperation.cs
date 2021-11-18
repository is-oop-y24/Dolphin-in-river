namespace Banks
{
    public class CreateAddMoneyOperation : ICreatorTransaction
    {
        private AbstractAccount _account;
        private double _money;

        public CreateAddMoneyOperation(AbstractAccount account, double money)
        {
            _account = account;
            _money = money;
        }

        public ITransaction Create()
        {
            return new AddMoney(_account, _money);
        }
    }
}