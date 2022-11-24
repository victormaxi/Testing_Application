

namespace TestProject
{
    public class ShoppingCartServiceFake : IShoppingCartService
    {
        private readonly List<ShoppingItem> shoppingCart;

        public ShoppingCartServiceFake()
        {
            this.shoppingCart = new List<ShoppingItem>()
            {
                new ShoppingItem()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    Name = "Orange Juice", Manufacturer = "Kelechi Group", Price = 5.00M
                },
                 new ShoppingItem()
                {
                    Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    Name = "Frozen Pizza", Manufacturer = "Kelechi Group", Price = 12.00M
                }
            };
        }

        public IEnumerable<ShoppingItem> GetAllItems()
        {
            return this.shoppingCart;
        }

        public ShoppingItem Add(ShoppingItem newItem)
        {
            newItem.Id = Guid.NewGuid();
            this.shoppingCart.Add(newItem);
            return newItem;
        }

        public ShoppingItem GetById(Guid id)
        {
            return this.shoppingCart.Where(a => a.Id == id).FirstOrDefault();
        }
            
        public void Remove(Guid id)
        {
            var existing = this.shoppingCart.First(a=> a.Id == id);
            this.shoppingCart.Remove(existing);
        }
    }
}
