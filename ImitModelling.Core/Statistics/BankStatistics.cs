using Lab14.Agents;
using System;

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

        public void PrintFinalReport(double simulationEndTime, int totalTellers)
        {
            // Обновляем загрузку операторов вплоть до конца моделирования
            UpdateBusyTime(simulationEndTime);

            Console.WriteLine("===== Bank Simulation Report =====");
            Console.WriteLine($"Simulation time: {simulationEndTime:F2}");
            Console.WriteLine($"Total customers arrived: {ArrivalCustomers}");
            Console.WriteLine($"Total served customers: {ServedCustomers}");
            if (ServedCustomers > 0)
            {
                Console.WriteLine($"Average waiting time in queue: {TotalWaitingTime / ServedCustomers:F2}");
                Console.WriteLine($"Average time in bank: {TotalTimeInBank / ServedCustomers:F2}");
            }
            Console.WriteLine();

            Console.WriteLine($"Total teller-busy-time (sum over all tellers): {CumulativeBusyTime:F2}");
            Console.WriteLine($"Average teller utilization: {CumulativeBusyTime / (totalTellers * simulationEndTime):F4} ");
            Console.WriteLine("==================================");
        }
    }
}
