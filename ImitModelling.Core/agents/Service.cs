using ImitModelling.Core.Statistics;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab14.Agents
{
    public class Service : Agent
    {
        private readonly int _id;
        private bool isBusy = false;
        private Customer currentCustomer = null;
        private List<Customer> customers = new List<Customer>();

        public Service(Simulation system, int id,double startTime = 0.0) : base(system)
        {
            _id = id;
            NextEventTime = startTime;
        }

        public override void ProcessEvent(double currentTime)
        {
            if (currentTime < NextEventTime)
                return;


            if (isBusy)
            {
                isBusy = false;
                currentCustomer.IsServing = true;
                BankStatistics.Instance.RecordDeparture(currentCustomer, currentTime);
                BankStatistics.Instance.TellerFinishesService(currentTime);
                currentCustomer = null;
            }

            if (_system.QueueCount > 0)
            {
                Customer nextCust = _system.DequeueCustomer();
                currentCustomer = nextCust;
                customers.Add(nextCust);
                nextCust.StartService(currentTime);
                NextEventTime = nextCust.NextEventTime;
                isBusy = true;
                BankStatistics.Instance.TellerStartsService(currentTime);
            }
            else
            {
                NextEventTime = 0.0;
            }

        }
    }
}