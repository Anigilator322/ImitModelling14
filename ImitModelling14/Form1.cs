using ImitModelling.Core.Statistics;
using Lab14;
using System;
using System.Windows.Forms;

namespace ImitModelling14
{
    public partial class Form1 : Form
    {
        private Simulation simulation;
        private double stopTime;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            simulation = new Simulation(int.Parse(numericUpDown3.Value.ToString()),
                double.Parse(numericUpDown1.Value.ToString()), 
                double.Parse(numericUpDown2.Value.ToString()));
            stopTime = double.Parse(numericUpDown4.Value.ToString());
            timer1.Interval = 500;
            timer1.Start();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(simulation.CurrentTime >= stopTime)
            {
                timer1.Stop();
                Print();
                //MessageBox.Show("Simulation finished at time: " + simulation.CurrentTime);
                return;
            }
            Print();
            simulation.RunTick();
        }

        private void Print()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Current time: " + simulation.CurrentTime);
            foreach (var a in BankStatistics.Instance.GetReport(stopTime, int.Parse(numericUpDown3.Value.ToString())))
            {
                listBox1.Items.Add(a);
            }
        }
    }
}
