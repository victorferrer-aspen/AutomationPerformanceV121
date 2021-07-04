using Aspentech.HYSYS;
using Simulators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace TestDefinitions.MyTests
{
    public class ChangeSingleInput
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            //hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            SimulationCase simCase = (SimulationCase)hysysSimulator.GetActiveSimulationCase();
            Flowsheet flowsheet = simCase?.Flowsheet;//?.Flowsheets[0] as Flowsheet;
            ProcessStream stream = flowsheet.MaterialStreams["Crude_HCAMS"] as ProcessStream;
            List<TimeSpan> timeList = new List<TimeSpan>();

            double tempValue = stream.Temperature.Value;
            Stopwatch stopWatch = new Stopwatch();
            for (int i = 1; i <= 4; i++)
            {
                stopWatch.Start();
                if (i % 2 == 0)
                {
                    simCase.Solver.CanSolve = false;
                    stream.Temperature.Value = tempValue;
                    simCase.Solver.CanSolve = true;
                }
                else
                {
                    simCase.Solver.CanSolve = false;
                    stream.Temperature.Value = tempValue + 1;
                    simCase.Solver.CanSolve = true;
                }
                    
                stopWatch.Stop();
                timeList.Add(stopWatch.Elapsed);
            }

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(filePath,"CPUTime.csv")))
            {
                foreach(TimeSpan sw in timeList)
                {
                    outputFile.WriteLine(string.Join(",", sw.TotalSeconds));
                }
            }
                Console.WriteLine("test finished");

        }
    }
}
