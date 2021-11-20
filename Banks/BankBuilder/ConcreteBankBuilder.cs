namespace Banks.BankBuilder
{
    public class ConcreteBankBuilder : IBankBuilder
    {
        private readonly Bank _bank;

        public ConcreteBankBuilder()
        {
            _bank = new Bank();
        }

        public ConcreteBankBuilder(string bankName, double debitPercent, DepositInfo depositInfo, double creditLimit, double creditCommission, double criticalSum)
        {
            _bank = new Bank();
            BuildName(bankName).BuildDebitPercent(debitPercent).BuildDepositInfo(depositInfo)
                .BuildCreditLimit(creditLimit).BuildCreditCommission(creditCommission).BuildCriticalSum(criticalSum);
        }

        public IBankBuilder BuildName(string bankName)
        {
            _bank.Name = bankName;
            return this;
        }

        public IBankBuilder BuildDebitPercent(double debitPercent)
        {
            _bank.SetDebitPercent(debitPercent);
            return this;
        }

        public IBankBuilder BuildDepositInfo(DepositInfo depositInfo)
        {
            _bank.SetDepositInfo(depositInfo);
            return this;
        }

        public IBankBuilder BuildCreditLimit(double creditLimit)
        {
            _bank.SetCreditLimit(creditLimit);
            return this;
        }

        public IBankBuilder BuildCreditCommission(double creditCommission)
        {
            _bank.SetCreditCommission(creditCommission);
            return this;
        }

        public IBankBuilder BuildCriticalSum(double criticalSum)
        {
            _bank.SetCriticalSum(criticalSum);
            return this;
        }

        public Bank GetResult()
        {
            return _bank;
        }
    }
}