using System;
using System.Collections.Generic;

namespace Banks
{
    public class Bank
    {
        private static int nextId = 1;
        private List<Client> _clients = new List<Client>();

        public Bank(string bankName, double debitPercent, DepositInfo depositInfo, double creditLimit, double creditCommission, double criticalSum)
        {
            Id = nextId++;
            Name = bankName;
            Data = new BankData(debitPercent, depositInfo, creditLimit, creditCommission, criticalSum);
        }

        public int Id
        {
            get;
            private set;
        }

        public BankData Data
        {
            get;
        }

        public string Name
        {
            get;
        }

        public void ChangeDebitInfo(double debitPercent)
        {
            Data.SetDebitPercent(debitPercent);
            NotifyObservers("Debit");
        }

        public void ChangeDepositInfo(DepositInfo depositInfo)
        {
            Data.SetDepositPercent(depositInfo);
            NotifyObservers("Deposit");
        }

        public void ChangeCreditInfo(double creditLimit, double creditCommission)
        {
            Data.SetCreditInfo(creditLimit, creditCommission);
            NotifyObservers("Credit");
        }

        public void ChangeCriticalSum(double criticalSum)
        {
            Data.SetCriticalSum(criticalSum);
            NotifyObservers("AllType");
        }

        public List<Client> GetClients()
        {
            return _clients;
        }

        public List<string> GetClientsNames()
        {
            var clientsName = new List<string>();
            foreach (Client client in _clients)
            {
                string personalInformation = client.GetPersonalInformation();
                clientsName.Add(personalInformation);
            }

            return clientsName;
        }

        public void NotifyObservers(string typeAccount)
        {
            foreach (Client client in _clients)
            {
                foreach (AbstractAccount item in client.GetAccounts())
                {
                    if (item.TypeAccount == typeAccount || typeAccount == "AllType")
                    {
                        client.NotifyChangesConditions();
                        client.ChangeConditionInBank(Data);
                        break;
                    }
                }
            }
        }

        public AbstractAccount AddClientAndLinkAccount(Client client, AbstractCreateAccount createAccount)
        {
            createAccount.AddData(Data);
            AbstractAccount newAccount = createAccount.Create(client);
            client.AddAccount(newAccount);
            _clients.Add(client);
            return newAccount;
        }

        public void NotifyCalculates(DateTime newDate)
        {
            foreach (Client client in _clients)
            {
                client.UpdateInformationAboutMoneyInAccounts(newDate);
            }
        }

        public bool IsAccount(AbstractAccount account)
        {
            bool flag = false;
            foreach (Client client in _clients)
            {
                foreach (AbstractAccount curAccount in client.GetAccounts())
                {
                    if (account.GetId() == curAccount.GetId())
                    {
                        flag = true;
                    }
                }
            }

            return flag;
        }
    }
}