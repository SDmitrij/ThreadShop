using System;
using System.Collections.Generic;
using System.Threading;

namespace ThreadShop.Models
{
    public class CashRegister
    {
        public string Id;

        private readonly Queue<Customer> _cashQueueForAll = new Queue<Customer>();
        private readonly Queue<Customer> _cashQueueForPersonal = new Queue<Customer>();
        private readonly object _locker = new object();
        
        public CashRegister() => Id = Guid.NewGuid().ToString();

        public void Accept(Customer customer)
        {
            lock (_locker)
            {
                if (customer.ShopPersonal)
                {
                    _cashQueueForPersonal.Enqueue(customer);
                    Monitor.Pulse(_locker);
                }
                else
                {
                    _cashQueueForAll.Enqueue(customer);
                    Monitor.Pulse(_locker);
                }
            }
        }

        public void Service()
        {
            lock (_locker)
            {
                while (_cashQueueForPersonal.Count == 0 && _cashQueueForAll.Count == 0)
                    Monitor.Wait(_locker);
                if (_cashQueueForPersonal.Count > 0)
                {
                    var cust = _cashQueueForPersonal.Dequeue();
                    Console.WriteLine("\tShop personal customer {0} {1} has being serviced", cust.Id, cust.Name);
                }
                else if (_cashQueueForPersonal.Count == 0 && _cashQueueForAll.Count > 0)
                {
                    var cust = _cashQueueForAll.Dequeue();
                    Console.WriteLine("\tCustomer {0} {1} has being serviced", cust.Id, cust.Name);
                }
            }
        }
    }
}
