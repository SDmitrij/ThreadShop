using System;
using System.Threading;

namespace ThreadShop.Models
{
    public class Shop
    {
        private const int NumOfCustomers = 10;

        public void Run()
        {
            CashRegister cashRegister = new CashRegister();
            void processCustomers()
            {
                for (int i = 0; i < NumOfCustomers; i++)
                {
                    cashRegister.Service();
                    Thread.Sleep(2000);
                }
            }
            new Thread(new ThreadStart(processCustomers)).Start();
            for (int i = 0; i < NumOfCustomers; i++)
            {
                var customer = new Customer { Name = $"Dmitry {i}", ShopPersonal = i % 2 == 0 };
                Console.WriteLine("Customer (shop personal: {0}), {1} {2}, entering shop", customer.ShopPersonal, customer.Id, customer.Name);
                cashRegister.Accept(customer);
                Thread.Sleep(2000);
            }
        }
    }
}