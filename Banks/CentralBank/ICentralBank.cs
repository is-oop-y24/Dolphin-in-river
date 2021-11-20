using System;
using System.Collections.Generic;

namespace Banks
{
    public interface ICentralBank
    {
        void UpdateMoneyInformation(DateTime newDate);
        ITransaction MakeTransaction(IFactoryCreatorTransaction creatorTransaction);
        void CancelTransaction(ITransaction transaction);
        List<Bank> GetBanks();
        List<string> GetBanksNames();
        List<string> GetNamesTransaction();
        List<ITransaction> GetTransactions();
        AbstractAccount AddClientAndLinkAccount(Bank bank, Client client, FactoryAbstractCreateAccount createAccount);
        Bank AddBank(string bankName, double debitPercent, DepositInfo depositInfo, double creditLimit, double creditCommission, double criticalSum);
    }
}