using ImitModelling.Core.Statistics;
using System;
using System.Threading;

namespace Lab14.Agents
{
    public class Service : Agent
    {
        private readonly int _id;
        private bool isBusy = false;
        private Customer currentCustomer = null;

        public Service(Simulation system, int id,double startTime = 0.0) : base(system)
        {
            _id = id;
            NextEventTime = startTime;
        }

        public override void ProcessEvent(double currentTime)
        {
            if (currentTime < NextEventTime)
                return;

            double t = currentTime;

            if (isBusy)
            {
                isBusy = false;
                currentCustomer.IsServed = true;
                BankStatistics.Instance.RecordDeparture(currentCustomer, t);
                BankStatistics.Instance.TellerFinishesService(t);
                currentCustomer = null;
            }

            if (_system.QueueCount > 0)
            {
                Customer nextCust = _system.DequeueCustomer();
                currentCustomer = nextCust;

                nextCust.StartService(t);
                NextEventTime = nextCust.NextEventTime;
                isBusy = true;
                BankStatistics.Instance.TellerStartsService(t);
            }
            else
            {
                NextEventTime = 0.0;
            }

        }
    }
}