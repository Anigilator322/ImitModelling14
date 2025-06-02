using System;
using System.Threading;

namespace Lab14.Agents
{
    public class Service : Agent
    {
        private readonly int _id;
        //private BankStatistics stats;
        private bool isBusy = false;
        private Customer currentCustomer = null;

        public Service(Simulation system, int id,double startTime = 0.0) : base(system)
        {
            _id = id;
            NextEventTime = startTime;
        }

        public override void ProcessEvent()
        {
            double t = NextEventTime;

            if (isBusy)
            {
                isBusy = false;
                currentCustomer.IsServed = true;
                currentCustomer = null;
            }

            if (_system.QueueCount > 0)
            {
                Customer nextCust = _system.DequeueCustomer();
                currentCustomer = nextCust;

                nextCust.StartService(t);
                NextEventTime = nextCust.NextEventTime;
                isBusy = true;
                //stats.TellerStartsService(t);
            }
            else
            {
                NextEventTime = 0.0;
            }

        }
    }
}