using System;
namespace Banks
{
    public class CreateDepositAccount : AbstractCreateAccount
    {
        private DateTime _finishDay;
        public CreateDepositAccount(double money, DateTime finishDay)
            : base(money)
        {
            _finishDay = finishDay;
        }

        public override AbstractAccount Create(Client client)
        {
            CheckCorrectData();
            return new DepositAccount(Data, Money, _finishDay, client);
        }
    }
}