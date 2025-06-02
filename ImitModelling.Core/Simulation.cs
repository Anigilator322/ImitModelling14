using System;
using System.Collections.Generic;
using System.Linq;
using ImitModelling.Core;
using Lab14.Agents;

namespace Lab14
{
    public class Simulation
    {
        private List<Agent> agents = new List<Agent>();
        private PriorityQueue eventQueue;
        private double stopTime;
        //private BankStatistics stats;

        // Очередь клиентов
        private Queue<Customer> customerQueue = new Queue<Customer>();

        public double CurrentTime { get; private set; } = 0.0;

        public Simulation(double stopTime, int totalServices, double lambda)
        {
            this.stopTime = stopTime;
            //stats = new BankStatistics();

            var source = new Source(this,lambda);
            RegisterAgent(source);

            for (int i = 0; i < totalServices; i++)
            {
                var service = new Service(this, i);
                RegisterAgent(service);
            }

            eventQueue = new PriorityQueue(
                agents.Select(a => (a, a.NextEventTime)).ToDictionary(x => x.a, x => x.Item2)
            );
        }

        public void RegisterAgent(Agent a)
        {
            agents.Add(a);
            if (eventQueue != null)
                eventQueue.Enqueue(a, a.NextEventTime);
        }

        public void EnqueueCustomer(Customer cust)
        {
            customerQueue.Enqueue(cust);
        }

        public Customer DequeueCustomer()
        {
            return customerQueue.Dequeue();
        }

        public int QueueCount => customerQueue.Count;

        private void UpdateAgentEventTime(Agent a)
        {
            eventQueue.UpdateValue(a, a.NextEventTime);
        }
        public void RunTick()
        {
            //RebuildEventQueue();
            for(int i = 0; i < agents.Count; i++)
            {
                if (CurrentTime >= stopTime)
                    break;
                agents[i].ProcessEvent(CurrentTime);
            }
            CurrentTime += 0.1;
        }

        private void RebuildEventQueue()
        {
            eventQueue = new PriorityQueue();
            foreach (var a in agents)
            {
                eventQueue.Enqueue(a, a.NextEventTime);
            }
        }

    }
}