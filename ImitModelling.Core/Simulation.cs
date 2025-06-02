using System;
using System.Collections.Generic;
using Lab14.Agents;

namespace Lab14
{
    public class Simulation
    {
        private readonly List<Agent> _agents;
        private double _currentTime;
        private readonly double _simulationTime;

        public Simulation(double simulationTime, double interArrivalTimeMean, double serviceTimeMean)
        {
            _simulationTime = simulationTime;
            _currentTime = 0;
            _agents = new List<Agent>();

            // Create and connect agents
            var source = new Source("Source", 1.0 / interArrivalTimeMean);
            var queue = new Queue("Queue");
            var service = new Service("Service", 1.0 / serviceTimeMean);
            var output = new Output("Output");

            // Connect agents through events
            source.CustomerGenerated += customer => queue.Enqueue(customer);
            queue.CustomerDequeued += customer => service.ProcessCustomer(customer, _currentTime);
            service.CustomerServiced += customer => output.ProcessCustomer(customer);

            // Add agents to the list
            _agents.Add(source);
            _agents.Add(queue);
            _agents.Add(service);
            _agents.Add(output);
        }

        public void Run()
        {
            while (_currentTime < _simulationTime)
            {
                // Generate new customer if it's time
                if (_agents[0] is Source source && source.ShouldGenerateCustomer(_currentTime))
                {
                    source.GenerateCustomer(_currentTime);
                }

                var queue = _agents[1] as Queue;
                var service = _agents[2] as Service;
                if (!service.IsBusy && queue.Count > 0)
                {
                    var customer = queue.Dequeue();
                    service.ProcessCustomer(customer, _currentTime);
                }

                // Advance time
                _currentTime += 0.1;
                System.Threading.Thread.Sleep(50);
            }

            while ((_agents[1] as Queue).Count > 0)
            {
                var queue = _agents[1] as Queue;
                var service = _agents[2] as Service;
                if (!service.IsBusy)
                {
                    var customer = queue.Dequeue();
                    service.ProcessCustomer(customer, _currentTime);
                }
                _currentTime += 0.1;
                System.Threading.Thread.Sleep(50);
            }

            PrintResults();
        }

        private void PrintResults()
        {
            var output = _agents[3] as Output;
            //Console.WriteLine("\nSimulation Results:");
            //Console.WriteLine($"Total customers processed: {output.TotalProcessedCustomers}");
            //Console.WriteLine($"Average queue time: {output.AverageQueueTime:F2}");
            //Console.WriteLine($"Average total time: {output.AverageTotalTime:F2}");
        }
    }
}