namespace Banks
{
    public class BankData
    {
        public BankData(double debitPercent, DepositInfo depositPercent, double creditLimit, double creditCommission, double criticalSum)
        {
            DebitPercent = debitPercent;
            InfoDeposit = depositPercent;
            CreditLimit = creditLimit;
            CreditCommission = creditCommission;
            CriticalSum = criticalSum;
        }

        public double CreditLimit
        {
            get;
            private set;
        }

        public double CreditCommission
        {
            get;
            private set;
        }

        public double CriticalSum
        {
            get;
            private set;
        }

        public double DebitPercent
        {
            get;
            private set;
        }

        public DepositInfo InfoDeposit
        {
            get;
            private set;
        }

        public void SetDebitPercent(double debitPercent)
        {
            DebitPercent = debitPercent;
        }

        public void SetDepositPercent(DepositInfo depositInfo)
        {
            InfoDeposit = depositInfo;
        }

        public void SetCreditInfo(double creditLimit, double creditCommission)
        {
            CreditLimit = creditLimit;
            CreditCommission = creditCommission;
        }

        public void SetCriticalSum(double criticalSum)
        {
            CriticalSum = criticalSum;
        }
    }
}