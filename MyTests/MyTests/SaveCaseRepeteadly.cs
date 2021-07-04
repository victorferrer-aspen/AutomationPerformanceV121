using Simulators;
using System;

namespace TestDefinitions.MyTests
{
    public class SaveCaseRepeteadly
    {
        //static string filePath = Environment.CurrentDirectory; //@"C:\Users\FERRERLV\Desktop\Performance V12-1\"//requires installation of CSharp package
        public void TestDefinition(string fileName, string filePath, ISimulator hysysSimulator)
        {
            bool isOpen = hysysSimulator.OpenCase(new CaseInfo(filePath, fileName));
            if (isOpen)
            {
                for (int i = 0; i < 10; i++)
                    hysysSimulator.SaveCase();
            }
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
