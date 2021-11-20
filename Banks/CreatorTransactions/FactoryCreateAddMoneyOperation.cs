namespace Banks
{
    public class FactoryCreateAddMoneyOperation : IFactoryCreatorTransaction
    {
        private AbstractAccount _account;
        private double _money;

        public FactoryCreateAddMoneyOperation(AbstractAccount account, double money)
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