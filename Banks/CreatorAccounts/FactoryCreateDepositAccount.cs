using System;
namespace Banks
{
    public class FactoryCreateDepositAccount : FactoryAbstractCreateAccount
    {
        private DateTime _finishDay;
        public FactoryCreateDepositAccount(double money, DateTime finishDay)
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