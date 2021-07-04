using System;
using Simulators;
namespace Services
{
    public class MonitorService
    {
        private PersistenceManager persistenceManager;
        private ConnectionService connection;
        public bool Connected { get; private set; }
        public MonitorService(SimulatorInfo simInfo, string filePath, int frequency)
        {
            persistenceManager = new PersistenceManager(filePath, simInfo);
            connection = new ConnectionService(frequency);
            Connected = false;
        }
        public void Connect()
        {
            connection.MessagedArrived += new EventHandler<ConnectionEventArgs>(persistenceManager.WriteMemoryUsage);
            connection.Connect();
            Connected = true;
        }

        public void Disconect()
        {
            connection.Disconect();
            connection.MessagedArrived -= persistenceManager.WriteMemoryUsage;
            Connected = false;
        }
    }
}
