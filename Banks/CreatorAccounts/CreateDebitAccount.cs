namespace Banks
{
    public class CreateDebitAccount : AbstractCreateAccount
    {
        public CreateDebitAccount(double money)
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