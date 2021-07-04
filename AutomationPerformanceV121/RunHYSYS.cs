using AutomationPerformance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AutomationPerformanceV121
{
    class RunHYSYS
    {
        static double TemperatureIncrement = 0.01;
        static string fileName = @"GAS PLANT MODEL - No Case Study V121.hsc";
        public void RunHYSYSAndMonitoring(string relativefilePath)
        {
            HYSYS.Application hyApp = new HYSYS.Application();
            hyApp.Visible = true;
            string shortVersion = hyApp.Version;
            string longVersion = hyApp.LongVersion;

            HYSYS.SimulationCase simCase = hyApp.SimulationCases.Open(Path.Combine(relativefilePath, fileName));
            simCase.Visible = true;

            HYSYS.BackDoor bd = (HYSYS.BackDoor)simCase;
            dynamic processId = bd.get_BackDoorVariable(":MultiCaseProcessId.0").Variable;
            string processIdString = processId.Value;

            HYSYS.ProcessStream streamGasInlet = simCase.Flowsheet.Streams["Inlet Gas"];
            double initialTemperature = streamGasInlet.Temperature.GetValue();

            PersistenceManager persistenceManager = new PersistenceManager(relativefilePath, $"PerformanceMonitor{shortVersion}.csv", longVersion, Int32.Parse(processIdString));
            Connection connection = new Connection(15000);
            connection.MessagedArrived += new EventHandler(persistenceManager.WriteMemoryUsage);
            connection.Connect();
            for (int i = 1; i <= 1000; i++)
            {
                streamGasInlet.Temperature.Value = initialTemperature + TemperatureIncrement * i;
                Console.WriteLine($"---Iteration Completed {i}");
            }
            Console.WriteLine("Iterations completed");
            Console.ReadKey();
            connection.Disconect();
        }
    }
}
