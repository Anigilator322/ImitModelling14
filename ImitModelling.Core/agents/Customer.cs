using ImitModelling.Core.Statistics;
using System;

namespace Lab14.Agents
{
    public class Customer : Agent
    {
        public int id;
        public double ArrivalTime { get; set; }
        public double ServiceStartTime { get; private set; }
        public bool IsServing = false;
        private PoissonDistribution poissonDistribution;
        public Customer(Simulation system, double mu = 1.0) : base(system)
        {
            poissonDistribution = new PoissonDistribution(mu);
            ArrivalTime = _system.CurrentTime;
        }

        public override void ProcessEvent()
        {
            if (IsServing)
                return;

            _system.EnqueueCustomer(this);
            NextEventTime = Double.PositiveInfinity;
        }

        public double StartService()
        {
            ServiceStartTime = _system.CurrentTime;
            double serviceDuration = SampleServiceTime();
            IsServing = true;
            return serviceDuration;
        }

        private double SampleServiceTime()
        {
            return poissonDistribution.Generate();
        }
    }
}