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
        private Queue<Customer> customerQueue = new Queue<Customer>();

        public double CurrentTime { get; private set; } = 0.0;

        public Simulation(int totalServices, double lambda, double lambdaServices)
        {
            var source = new Source(this,lambda, lambdaServices);
            RegisterAgent(source);

            for (int i = 0; i < totalServices; i++)
            {
                var service = new Service(this, i);
                RegisterAgent(service);
            }
        }

        public void RegisterAgent(Agent a)
        {
            agents.Add(a);
            
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
        public void RunTick()
        {
            for(int i = 0; i < agents.Count; i++)
            {
                agents[i].ProcessEvent(CurrentTime);
            }
            CurrentTime += 1;
        }
    }
}