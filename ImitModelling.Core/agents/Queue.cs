using System;
using System.Collections.Generic;

namespace Lab14.Agents
{
    public class Queue : Agent
    {
        private readonly Queue<Customer> _customers;
        public event Action<Customer> CustomerDequeued;

        public Queue(string name) : base(name)
        {
            _customers = new Queue<Customer>();
        }

        public void Enqueue(Customer customer)
        {
            _customers.Enqueue(customer);
        }

        public Customer Dequeue()
        {
            if (_customers.Count > 0)
            {
                var customer = _customers.Dequeue();
                CustomerDequeued?.Invoke(customer);
                return customer;
            }
            return null;
        }

        public int Count => _customers.Count;
    }
}