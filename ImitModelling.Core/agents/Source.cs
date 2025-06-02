using System;

namespace Lab14.Agents
{
    public class Source : Agent
    {
        private readonly PoissonDistribution _distribution;
        private double _nextGenerationTime;
        public event Action<Customer> CustomerGenerated;

        public Source(string name, double lambda) : base(name)
        {
            _distribution = new PoissonDistribution(lambda);
            _nextGenerationTime = 0;
        }

        public bool ShouldGenerateCustomer(double currentTime)
        {
            if (currentTime >= _nextGenerationTime)
            {
                _nextGenerationTime = currentTime + _distribution.Generate();
                return true;
            }
            return false;
        }

        public void GenerateCustomer(double currentTime)
        {
            var customer = new Customer($"Customer_{Guid.NewGuid().ToString().Substring(0, 8)}", currentTime);
            Console.WriteLine($"[t={currentTime:F2}] {customer.Name} arrived");
            CustomerGenerated?.Invoke(customer);
        }
    }
}