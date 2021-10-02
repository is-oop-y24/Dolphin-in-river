using System.Collections.Generic;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private static int _nextId = 0;
        private int _id;
        private string _name;
        private string _address;
        private Dictionary<int, Procurement> _storage = new Dictionary<int, Procurement>();

        public Shop(string name, string address)
        {
            _name = name;
            _address = address;
            _id = ++_nextId;
        }

        public void AddNewProduct(int price, int amount, Product product)
        {
            if (_storage.ContainsKey(product.GetId()))
            {
                int newAmount = -1 * amount; // ChengeAmount() decrease amount values
                _storage[product.GetId()].SetNewPrice(price);
                _storage[product.GetId()].ChangeAmount(newAmount);
            }
            else
            {
                var buff = new Procurement(product, price, amount);
                _storage.Add(product.GetId(), buff);
            }
        }

        public void BuyProduct(Person person, OrderForPerson order)
        {
            foreach (Procurement currProcurement in _storage.Values)
            {
                if (currProcurement.GetName().Equals(order.GetName()))
                {
                    if (person.GetMoney() < currProcurement.GetPrice() * order.GetAmount())
                    {
                        throw new ShopException("Not enough money to buy Product" + order.GetName());
                    }

                    int buffMoney = -person.GetMoney() + (currProcurement.GetPrice() * order.GetAmount());
                    currProcurement.ChangeAmount(order.GetAmount());
                    person.MoneyAfterBuy(buffMoney);
                    var newOrder = new OrderForPerson(order.GetAmount(), currProcurement.GetProduct());
                    person.AddOrder(newOrder);

                    return;
                }
            }

            throw new ShopException("This Product cannot be found in shop");
        }

        public int NewPrice(Product product, int newPrice)
        {
            foreach (Procurement currProcurement in _storage.Values)
            {
                if (currProcurement.GetId().Equals(product.GetId()))
                {
                    currProcurement.SetNewPrice(newPrice);
                    return currProcurement.GetPrice();
                }
            }

            throw new ShopException("This Product cannot be found in shop");
        }

        public int BuyBatchProduct(Person person, List<OrderForPerson> arrayPersonOrder)
        {
            int totalSum = 0;
            foreach (OrderForPerson currOrder in arrayPersonOrder)
            {
                bool find = true;
                foreach (Procurement currProcurement in _storage.Values)
                {
                    if (currProcurement.GetId() == currOrder.GetId())
                    {
                        find = false;
                        totalSum += currProcurement.GetPrice() * currOrder.GetAmount();
                        currProcurement.ChangeAmount(currOrder.GetAmount());
                    }
                }

                if (find)
                {
                    throw new ShopException("Product " + currOrder.GetName() + " cannot be found in shop");
                }

                if (totalSum > person.GetMoney())
                {
                    throw new ShopException("Not enough money to buy this list of Products");
                }
            }

            return person.MoneyAfterBuy(totalSum);
        }

        public bool IsProductWithId(Product product)
        {
            return _storage.ContainsKey(product.GetId());
        }

        public bool IsNeedAmount(Product product, int userAmount)
        {
            return _storage[product.GetId()].GetAmount() >= userAmount;
        }

        public int CurrentlySumParty(int idProduct, int amount)
        {
            return _storage[idProduct].GetPrice() * amount;
        }

        public int AmountProduct(Product product)
        {
            return _storage[product.GetId()].GetAmount();
        }

        public int GetId()
        {
            return _id;
        }
    }
}