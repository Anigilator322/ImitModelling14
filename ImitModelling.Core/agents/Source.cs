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
            // Если U равномерно(0,1), то -ln(U)/lambda — экспоненциально(lambda)
            double U = rnd.NextDouble();
            return -Math.Log(1.0 - U) / lambda;
        }


        public override void ProcessEvent(double currentTime) 
        {
            if (currentTime < NextEventTime)
                return;
            double t = NextEventTime;
            Customer newCust = new Customer(_system, t);
            _system.RegisterAgent(newCust);

            NextEventTime = t + SampleInterarrival();
        }

    }
}