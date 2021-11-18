using Banks.Tools;

namespace Banks
{
    public class WithDrawMoney : ITransaction
    {
        private static int _nextId = 0;
        private AbstractAccount _account;
        private double _money;
        private bool _isCanceled = false;
        private int _id;
        private string _type;

        public WithDrawMoney(AbstractAccount account, double money)
        {
            _account = account;
            _money = money;
            _id = _nextId++;
            _type = "WithDraw";
            DoTransact();
        }

        public void Cancel()
        {
            if (_isCanceled)
            {
                throw new BanksException("This transaction has already been cancelled");
            }

            _isCanceled = true;
            _account.AddMoney(_money);
        }

        public string GetInformation()
        {
            return _account.GetClient().GetName() + _id;
        }

        public string GetTypeTransaction()
        {
            return _type;
        }

        public int GetId()
        {
            return _id;
        }

        private void DoTransact()
        {
            if (_money < 0)
            {
                throw new BanksException("Input data are incorrect");
            }

            _account.WithDraw(_money);
        }
    }
}