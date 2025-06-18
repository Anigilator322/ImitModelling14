using System;

namespace Lab14.Agents
{
    public abstract class Agent
    {
        public double NextEventTime { get; set; }
        protected readonly Simulation _system;
        protected Agent(Simulation system)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
        }
        public abstract void ProcessEvent();
    }
}