using System;
using System.IO;
using System.Threading.Tasks;
using Aspentech.HYSYS;
using Microsoft.VisualBasic;

namespace Simulators
{
    public class HysysSimulator : ISimulator, IDisposable
    {
        private Application hyApp;
        private SimulationCase simCase;
        public CaseInfo caseInfo;
        public SimulatorInfo simInfo;
        public event EventHandler<SimulatorEventArgs> SimulationCaseIsOpen;
        public bool CreateSimulator()
        {
            hyApp = new Application();
            hyApp.Visible = true;
            BackDoor bd = (BackDoor)hyApp;
            //dynamic processId = bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
            //simInfo = new SimulatorInfo(hyApp.Version, hyApp.LongVersion, Int32.Parse(processId.Value));
            if (hyApp == null)
                return false;
            else
                return true;    
        }
        public void CloseSimulator()
        {
            hyApp?.Quit();
            hyApp = null;
        }
        public bool OpenCase(CaseInfo caseInfo)
        {
            this.caseInfo = caseInfo;
            simCase = hyApp.SimulationCases.Open(caseInfo.FullPath) as SimulationCase;
            if (simCase == null)
            {
                return false;
            } 
            else
            {
                simCase.Visible = true;
                return true;

            }
        }
        public void SaveCase()
        {
            simCase.Save();
        }
        public void CloseCase()
        {
            simCase.Close();
        }
        public bool Run()
        {
            throw new NotImplementedException();
        }

        public int GetProcessId()
        {
            BackDoor bd = (BackDoor)simCase;
            dynamic processId = bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
            return Int32.Parse(processId.Value);
        }

        public void Dispose()
        {
            simCase?.Close();
            simCase = null;
            hyApp?.Quit();
            hyApp = null;
        }

        protected virtual void SimulatiionCaseOpened(SimulatorEventArgs e)
        {
            EventHandler<SimulatorEventArgs> handler = SimulationCaseIsOpen;
            handler?.Invoke(this, e);
        }

        public _SimulationCase GetActiveSimulationCase()
        {
            
            return simCase??hyApp.ActiveDocument;
        }
        public dynamic GetCaseVariable(string moniker)
        {
            BackDoor bd = (BackDoor)simCase;
            return bd?.get_BackDoorVariable(moniker).Variable??null;
        }
    }

    public class SimulatorEventArgs : EventArgs
    {
        public SimulatorInfo SimInfo { get; set; }
    }
}
