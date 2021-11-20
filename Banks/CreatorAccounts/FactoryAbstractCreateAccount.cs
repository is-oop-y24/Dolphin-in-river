using Banks.Tools;

namespace Banks
{
    public abstract class FactoryAbstractCreateAccount
    {
        protected FactoryAbstractCreateAccount(double money)
        {
            Money = money;
        }

        protected BankData Data
        {
            get;
            private set;
        }

        protected double Money
        {
            get;
        }

        public abstract AbstractAccount Create(Client client);

        public void AddData(BankData data)
        {
            Data = data;
        }

        protected void CheckCorrectData()
        {
            if (Data == null)
            {
                throw new BanksException("Data hasn't been entered");
            }
        }
    }
}