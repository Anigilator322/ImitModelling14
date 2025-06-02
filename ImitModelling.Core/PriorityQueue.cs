using Lab14.Agents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ImitModelling.Core
{
    public class PriorityQueue
    {
        private class QueueItem
        {
            public Agent Agent { get; }
            public double Value { get; set; }

            public QueueItem(Agent agent, double value)
            {
                Agent = agent;
                Value = value;
            }
        }

        private readonly List<QueueItem> _items = new List<QueueItem>();

        public void Enqueue(Agent agent, double value)
        {
            _items.Add(new QueueItem(agent, value));
            Sort();
        }

        public Agent Dequeue()
        {
            if (_items.Count == 0)
                return null;

            var item = _items[0];
            _items.RemoveAt(0);
            return item.Agent;
        }

        public void UpdateValue(Agent agent, double newValue)
        {
            var item = _items.FirstOrDefault(i => ReferenceEquals(i.Agent, agent));
            if (item == null)
                return;

            item.Value = newValue;
            Sort();
        }

        public int Count => _items.Count;

        private void Sort()
        {
            _items.Sort((a, b) => a.Value.CompareTo(b.Value));
        }

        public bool Contains(Agent agent)
        {
            return _items.Any(i => ReferenceEquals(i.Agent, agent));
        }

        public PriorityQueue(Dictionary<Agent, double> initialItems)
        {
            foreach (var kvp in initialItems)
            {
                _items.Add(new QueueItem(kvp.Key, kvp.Value));
            }
            Sort();
        }

        public PriorityQueue()
        {
            _items = new List<QueueItem>();
        }
    }
}
