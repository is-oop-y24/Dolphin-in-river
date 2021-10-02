using System.Collections.Generic;

namespace Shops
{
    public interface IShopManager
    {
        public Shop AddShop(string name, string address);
        public Shop FindCheapBatch(Dictionary<int, Product> batch);
        public Product RegisterProduct(string name);
    }
}