namespace Banks
{
    public class FactoryCreateWithDrawOperation : IFactoryCreatorTransaction
    {
        private AbstractAccount _account;
        private double _money;

        public FactoryCreateWithDrawOperation(AbstractAccount account, double money)
        {
            _account = account;
            _money = money;
        }

        public ITransaction Create()
        {
            return new WithDrawMoney(_account, _money);
        }
    }
}