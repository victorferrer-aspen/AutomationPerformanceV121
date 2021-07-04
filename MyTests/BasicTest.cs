using Services;
using System;
using System.Threading.Tasks;
using Simulators;

namespace TestDefinitions
{
    public class BasicTest: ITest
    {
        public string FilePath;
        public string FileName;
        public HysysSimulator Simulator;
        public Action<string,string, ISimulator> Test = null;

        public bool OpenSimulator()
        {
            Simulator = new HysysSimulator();
            return Simulator.CreateSimulator();
        }
        public void CloseSimulator()
        {
            Simulator.Dispose();
        }
        public void RunTest()
        {
            Test(FilePath, FileName, Simulator);
        }

        public ISimulator GetSimulator()
        {
            return Simulator;
        }
    }
}
