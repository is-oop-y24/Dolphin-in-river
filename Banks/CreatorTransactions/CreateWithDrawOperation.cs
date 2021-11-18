namespace Banks
{
    public class CreateWithDrawOperation : ICreatorTransaction
    {
        private AbstractAccount _account;
        private double _money;

        public CreateWithDrawOperation(AbstractAccount account, double money)
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