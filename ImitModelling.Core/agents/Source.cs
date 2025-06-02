using ImitModelling.Core.Statistics;
using System;

namespace Lab14.Agents
{
    public class Source : Agent
    {
        private readonly double lambda;
        private readonly Random rnd;

        public Source(Simulation system, double lambda) : base(system)
        {
            this.lambda = lambda;
            rnd = new Random();
            NextEventTime = SampleInterarrival();
        }

        private double SampleInterarrival()
        {
            var dist = new PoissonDistribution(lambda);
            return 1 / dist.Generate();
        }


        public override void ProcessEvent(double currentTime) 
        {
            if (currentTime < NextEventTime)
                return;
            //double t = currentTime + NextEventTime;
            Customer newCust = new Customer(_system, currentTime, 0.3);
            _system.RegisterAgent(newCust);
            BankStatistics.Instance.RecordArrival(newCust);
            NextEventTime = currentTime + SampleInterarrival();
        }

    }
}