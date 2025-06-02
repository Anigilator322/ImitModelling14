using System;

namespace Lab14.Agents
{
    public abstract class Agent
    {
        public string Name { get; set; }

        public Action OnProcessed;

        protected abstract void Process();

        protected Agent(string name)
        {
            Name = name;
        }
    }
}