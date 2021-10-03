using System.Collections.Generic;

namespace Shops
{
    public interface IShopManager
    {
        Shop AddShop(string name, string address);
        Shop FindCheapBatch(Dictionary<int, Product> batch);
        Product RegisterProduct(string name);
    }
}