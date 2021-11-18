using System;
using Banks.Tools;

namespace Banks
{
    public class ConcreteClientBuilder : IClientBuilder
    {
        private readonly Client _client;

        public ConcreteClientBuilder()
        {
            _client = new Client();
        }

        public IClientBuilder BuildName(string name)
        {
            _client.AddName(name);
            return this;
        }

        public IClientBuilder BuildSurname(string surname)
        {
            _client.AddSurname(surname);
            return this;
        }

        public IClientBuilder BuildAddress(string address)
        {
            _client.AddAddress(address);
            return this;
        }

        public IClientBuilder BuildPassportNumber(int passportNumber)
        {
            _client.AddPassportNumber(passportNumber);
            return this;
        }

        public Client GetResult()
        {
            CheckRequiredFields();
            _client.CheckNotRequireFields();

            return _client;
        }

        private void CheckRequiredFields()
        {
            if (_client.GetName() == " " || _client.GetSurname() == " ")
            {
                throw new BanksException("Client don't fill in the required fields");
            }
        }
    }
}