using Services;
using Simulators;

namespace TestDefinitions
{
    public class TestWithMonitor : ITest
    {
        public ITest BasicTest;
        public int MonitoringFrequency;
        private MonitorService Monitor;

        public TestWithMonitor(ITest basicTest, int monitorFrequency)
        {
            BasicTest = basicTest;
            MonitoringFrequency = monitorFrequency;
        }
        public bool OpenSimulator()
        {
            bool result = BasicTest.OpenSimulator();
            ConnectMonitor();
            return result;
        }
        public void CloseSimulator()
        {
            DisconnectMonitor();
            BasicTest.CloseSimulator();
        }

        private void ConnectMonitor()
        {
            SimulatorInfo simInfo = ((BasicTest)BasicTest).Simulator.simInfo;
            string filePath = ((BasicTest)BasicTest).FilePath;
            Monitor = new MonitorService(simInfo, filePath, MonitoringFrequency);
            Monitor.Connect();
        }
        private void DisconnectMonitor()
        {
            if (Monitor?.Connected ?? false)
            {
                Monitor.Disconect();
            }
        }
        public void RunTest()
        {
            BasicTest.RunTest();
        }

    }
}
