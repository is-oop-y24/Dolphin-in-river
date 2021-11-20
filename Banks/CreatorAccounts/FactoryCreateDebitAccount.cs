namespace Banks
{
    public class FactoryCreateDebitAccount : FactoryAbstractCreateAccount
    {
        public FactoryCreateDebitAccount(double money)
            : base(money)
        {
        }

        public override AbstractAccount Create(Client client)
        {
            CheckCorrectData();

            return new DebitAccount(Data, Money, client);
        }
    }
}