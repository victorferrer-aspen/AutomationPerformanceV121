using TestDefinitions;
using TestDefinitions.MyTests;
using System;
using Simulators;

namespace AutomationPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicTest basicTest = new BasicTest
            {
                FilePath = @"C:\Users\FERRERLV\Desktop\Gas Plant Performance\",
                FileName = @"PLANT 15 V1.4.2.HSC",
                Test = new Action<string, string, ISimulator>(ChangeSingleInput.TestDefinition)
            };

            TestWithMonitor testWithMonitor = new TestWithMonitor(basicTest, 15000);

            ITest myTest = basicTest;
            myTest.OpenSimulator();
            myTest.RunTest();
            myTest.CloseSimulator();
        }
    }
}
