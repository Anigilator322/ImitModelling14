using ImitModelling.Core.Statistics;
using System;

namespace Lab14.Agents
{
    public class Customer : Agent
    {
        private readonly Random rnd;
        private readonly double mu;
        public double ArrivalTime { get; }
        public double ServiceStartTime { get; private set; }
        public bool IsServed = false;

        public Customer(Simulation system, double arrivalTime, double mu = 1.0) : base(system)
        {
            ArrivalTime = arrivalTime;
            this.mu = mu;
            rnd = new Random();
            NextEventTime = ArrivalTime;
        }

        public override void ProcessEvent(double currentTime)
        {
            if (currentTime < NextEventTime)
                return;

            if (IsServed)
                return;

            _system.EnqueueCustomer(this);
            NextEventTime = Double.PositiveInfinity;
        }

        public void StartService(double currentTime)
        {
            ServiceStartTime = currentTime;
            double serviceDuration = SampleServiceTime();
            NextEventTime = currentTime + serviceDuration;
        }

        private double SampleServiceTime()
        {
            double U = rnd.NextDouble();
            return -Math.Log(1.0 - U) / mu;
        }
    }
}