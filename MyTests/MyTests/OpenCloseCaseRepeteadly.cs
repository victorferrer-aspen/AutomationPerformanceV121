using Simulators;

namespace TestDefinitions.MyTests
{
    public class OpenCloseCaseRepeteadly
    {
        public static void TestDefinition(string filePath, string fileName, ISimulator hysysSimulator)
        {
            for (int i = 0; i < 10; i++)
            {
                if (hysysSimulator.OpenCase(new CaseInfo(filePath, fileName)))
                    hysysSimulator.CloseCase();
            }
        }
    }
}
