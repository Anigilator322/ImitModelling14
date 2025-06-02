using ImitModelling.Core.Statistics;
using System;

namespace Lab14.Agents
{
    public class Customer : Agent
    {
        public int id;
        public double ArrivalTime { get; }
        public double ServiceStartTime { get; private set; }
        public bool IsServing = false;
        private PoissonDistribution poissonDistribution;
        public Customer(Simulation system, double arrivalTime, double mu = 1.0) : base(system)
        {
            ArrivalTime = arrivalTime;
            poissonDistribution = new PoissonDistribution(mu);
            NextEventTime = ArrivalTime;
        }

        public override void ProcessEvent(double currentTime)
        {
            if (currentTime < NextEventTime)
                return;

            if (IsServing)
                return;

            _system.EnqueueCustomer(this);
            NextEventTime = Double.PositiveInfinity;
        }

        public void StartService(double currentTime)
        {
            ServiceStartTime = currentTime;
            double serviceDuration = SampleServiceTime();
            IsServing = true;
            NextEventTime = currentTime + serviceDuration;
        }

        private double SampleServiceTime()
        {
            return poissonDistribution.Generate();
        }
    }
}