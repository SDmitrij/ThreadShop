using System;

namespace ThreadShop.Models
{
    public class Customer
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public bool ShopPersonal { get; set; }
        public bool Serviced { get; set; }

        public Customer() => Id = Guid.NewGuid().ToString();
    }
}
