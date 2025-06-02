using System;
using System.Threading;

namespace Lab14.Agents
{
    public class Service : Agent
    {
        private readonly PoissonDistribution _distribution;
        private bool _isBusy;
        public event Action<Customer> CustomerServiced;

        public Service(string name, double lambda) : base(name)
        {
            _distribution = new PoissonDistribution(lambda);
        }

        public void ProcessCustomer(Customer customer, double currentTime)
        {
            if (_isBusy) return;

            _isBusy = true;
            customer.ServiceStartTime = currentTime;

            var serviceDuration = _distribution.Generate();
            customer.ServiceEndTime = currentTime + serviceDuration;

            Thread.Sleep(serviceDuration);
            CustomerServiced?.Invoke(customer);
            _isBusy = false;
        }

        public bool IsBusy => _isBusy;
    }
}