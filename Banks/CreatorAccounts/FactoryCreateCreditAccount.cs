namespace Banks
{
    public class FactoryCreateCreditAccount : FactoryAbstractCreateAccount
    {
        public FactoryCreateCreditAccount(double money)
            : base(money)
        {
        }

        public override AbstractAccount Create(Client client)
        {
            CheckCorrectData();

            return new CreditAccount(Data, Money, client);
        }
    }
}