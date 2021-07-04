using AutomationPerformance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationPerformanceV121
{
    class HYSYSMonitor
    {
        public void MonitorHYSYS(string relativefilePath)
        {
            HYSYS.Application hyApp = new HYSYS.Application();
            hyApp.Visible = true;
            string shortVersion = hyApp.Version;
            string longVersion = hyApp.LongVersion;

            HYSYS._SimulationCase simCase = hyApp.ActiveDocument;
            simCase.Visible = true;

            HYSYS.BackDoor bd = (HYSYS.BackDoor)simCase;
            dynamic processId = bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
            string processIdString = processId.Value;

            PersistenceManager persistenceManager = new PersistenceManager(Environment.CurrentDirectory, "SystemMonitorHYSYS_V121.csv", longVersion, Int32.Parse(processIdString));
            Connection connection = new Connection(60000);
            connection.MessagedArrived += new EventHandler(persistenceManager.WriteMemoryUsage);
            connection.Connect();

            Console.ReadKey();
            connection.Disconect();
        }
    }
}
