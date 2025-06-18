using ImitModelling.Core.Statistics;
using System;

namespace Lab14.Agents
{
    public class Source : Agent
    {
        public event Action Notify;
        private PoissonDistribution poissonDistribution;
        private double lambdaForCustomer;
        public Source(Simulation system, double lambda, double lambda2) : base(system)
        {
            poissonDistribution = new PoissonDistribution(lambda);
            lambdaForCustomer = lambda2;
            NextEventTime = SampleInterarrival();
        }

        private double SampleInterarrival()
        {
            return poissonDistribution.Generate();
        }


        public override void ProcessEvent() 
        {
            Customer newCust = new Customer(_system, lambdaForCustomer);
            _system.RegisterAgent(newCust);
            BankStatistics.Instance.RecordArrival(newCust);
            NextEventTime = SampleInterarrival();
            Notify.Invoke();
        }

    }
}