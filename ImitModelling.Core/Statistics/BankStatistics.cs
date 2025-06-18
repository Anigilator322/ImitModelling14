using Lab14.Agents;
using System;
using System.Collections.Generic;

namespace ImitModelling.Core.Statistics
{
    public class BankStatistics
    {
        public double TotalWaitingTime = 0.0;
        public double TotalTimeInBank = 0.0;
        public int ServedCustomers = 0;

        private double lastUpdateTime = 0.0;
        public double CumulativeBusyTime = 0.0;

        public int BusyTellers = 0;
        public int ArrivalCustomers = 0;
        private BankStatistics()
        {
        }
        private static BankStatistics _instance;
        public static BankStatistics Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BankStatistics();
                }
                return _instance;
            }
        }

        public void UpdateBusyTime(double currentTime)
        {
            double dt = currentTime - lastUpdateTime;
            if (dt > 0)
            {
                CumulativeBusyTime += BusyTellers * dt;
                lastUpdateTime = currentTime;
            }
        }

        public void TellerStartsService(double currentTime)
        {
            UpdateBusyTime(currentTime);
            BusyTellers++;
        }

        public void TellerFinishesService(double currentTime)
        {
            UpdateBusyTime(currentTime);
            BusyTellers--;
        }

        public void RecordDeparture(Customer cust, double departureTime)
        {
            ServedCustomers++;
            double waitTime = cust.ServiceStartTime - cust.ArrivalTime;
            double timeInBank = departureTime - cust.ArrivalTime;
            TotalWaitingTime += waitTime;
            TotalTimeInBank += timeInBank;
        }

        public void RecordArrival(Customer cust)
        {
            ArrivalCustomers++;

        }

        public string[] GetReport(int totalTellers)
        {
            var report = new List<string>
            {
                "===== Bank Simulation Report =====",
                $"Total customers arrived: {ArrivalCustomers}",
                $"Total served customers: {ServedCustomers}"
            };
            if (ServedCustomers > 0)
            {
                report.Add($"Average waiting time in queue: {TotalWaitingTime / (ServedCustomers * totalTellers):F2}");
                report.Add($"Average time in bank: {TotalTimeInBank / (ServedCustomers * totalTellers):F2}");
            }
            report.Add($"Total teller-busy-time (sum over all tellers): {CumulativeBusyTime:F2}");
            report.Add("==================================");

            return report.ToArray();
        }

        public void ClearReport()
        {
            TotalWaitingTime = 0.0;
            TotalTimeInBank = 0.0;
            ServedCustomers = 0;

            lastUpdateTime = 0.0;
            CumulativeBusyTime = 0.0;

            BusyTellers = 0;
            ArrivalCustomers = 0;
        }
    }
}
