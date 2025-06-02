using System;

namespace Lab14
{
    public interface IDistribution
    {
        int Generate();
    }

    public class PoissonDistribution : IDistribution
    {
        private readonly double _lambda;

        public PoissonDistribution(double lambda)
        {
            _lambda = lambda;
        }
        
        private readonly Random _rand = new Random();
        
        public int Generate()
        {
            var L = Math.Exp(-_lambda);
            var k = 0;
            double p = 1;
            while (p > L)
            {
                k++;
                p *= _rand.NextDouble();
            }
            
            return k-1;
        }

    }
}