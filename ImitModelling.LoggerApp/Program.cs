using ImitModelling.Core.Statistics;
using Lab14;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImitModelling.LoggerApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sim = new Simulation(1, 0.1, 0.1);
            sim.RunTick();
            while (sim.CurrentTime < 100)
            {
                sim.RunTick();
            }

            var report = BankStatistics.Instance.GetReport(3);
            foreach (var s in report)
            {
                Console.WriteLine(s);
            }
        }

        
    }
}
