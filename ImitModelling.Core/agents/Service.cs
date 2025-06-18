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

        public Service(Simulation system, int id) : base(system)
        {
            _id = id;
            NextEventTime = Double.PositiveInfinity;
        }

        public override void ProcessEvent()
        {
            if (isBusy)
            {
                isBusy = false;
                currentCustomer.IsServing = true;
                BankStatistics.Instance.RecordDeparture(currentCustomer, _system.CurrentTime);
                BankStatistics.Instance.TellerFinishesService(_system.CurrentTime);
                currentCustomer = null;
            }

            if (_system.QueueCount > 0)
            {
                var nextCust = _system.DequeueCustomer();
                currentCustomer = nextCust;
                NextEventTime = nextCust.StartService();
                BankStatistics.Instance.TellerStartsService(_system.CurrentTime);
                isBusy = true;
            }
            else
            {
                NextEventTime = double.PositiveInfinity;
            }

        }
    }
}