namespace Lab14.Agents
{
    public class Customer : Agent
    {
        public double ArrivalTime { get; set; }
        public double ServiceStartTime { get; set; }
        public double ServiceEndTime { get; set; }
        public double QueueTime => ServiceStartTime - ArrivalTime;
        public double TotalTime => ServiceEndTime - ArrivalTime;

        public Customer(string name, double arrivalTime) : base(name)
        {
            ArrivalTime = arrivalTime;
        }
    }
}