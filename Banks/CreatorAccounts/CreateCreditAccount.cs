namespace Banks
{
    public class CreateCreditAccount : AbstractCreateAccount
    {
        public CreateCreditAccount(double money)
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