using ImitModelling.Core.Statistics;
using Lab14;
using System;
using System.Windows.Forms;

namespace ImitModelling14
{
    public partial class Form1 : Form
    {
        private Simulation simulation;
        public Form1()
        {
            InitializeComponent();
            
            timer1.Interval = 500;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                BankStatistics.Instance.ClearReport();
                listBox1.Items.Clear();
                button1.Text = "Simulate";
                return;
            }


            simulation = new Simulation(timer1.Interval / 1000.0,
                int.Parse(numericUpDown3.Value.ToString()),
                double.Parse(numericUpDown1.Value.ToString()), 
                double.Parse(numericUpDown2.Value.ToString()));
            
            timer1.Start();
            button1.Text = "Stop";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Print();
            simulation.RunTick();
        }

        private void Print()
        {
            listBox1.Items.Clear();
            foreach(var a in BankStatistics.Instance.GetReport(int.Parse(numericUpDown3.Value.ToString())))
            {
                listBox1.Items.Add(a);
            }
        }
    }
}
