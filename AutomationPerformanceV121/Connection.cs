using System;
using System.Timers;
using System.Diagnostics;

namespace AutomationPerformance
{
    class Connection
    {
        private Timer pollTimer;
        public event EventHandler MessagedArrived;
        public int Frequency { get; private set; }
        public int Counter { get; set; }

        public Connection(int frequency)
        {
            Frequency = frequency;
            pollTimer = new Timer(frequency);
            pollTimer.Elapsed += new ElapsedEventHandler(CheckForUpdate);
        }

        public void Connect()
        {
            pollTimer.Start();
        }
        public void Disconect() => pollTimer.Stop();

        private void CheckForUpdate(object source, EventArgs e)
        {
            MessagedArrived(this, null);
            this.Counter++;
        }
    }
}
