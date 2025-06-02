using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab14.Agents
{
    public class Output : Agent
    {
        private readonly List<Customer> _processedCustomers;
        public event Action<Customer> CustomerProcessed;

        public Output(string name) : base(name)
        {
            _processedCustomers = new List<Customer>();
        }

        public void ProcessCustomer(Customer customer)
        {
            _processedCustomers.Add(customer);
            CustomerProcessed?.Invoke(customer);
        }

        //public double AverageQueueTime => _processedCustomers.Count > 0 
        //    ? _processedCustomers.Average(c => c.QueueTime) 
        //    : 0;

        //public double AverageTotalTime => _processedCustomers.Count > 0 
        //    ? _processedCustomers.Average(c => c.TotalTime) 
        //    : 0;

        //public int TotalProcessedCustomers => _processedCustomers.Count;
    }
}