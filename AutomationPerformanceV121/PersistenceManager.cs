using System;
using System.IO;
using System.Linq;
using System.Diagnostics;

namespace AutomationPerformance
{
    class PersistenceManager
    {
        public PersistenceManager(string relativePath, string fileName, string hysysVersion, int processId)
        {
            FileName = fileName;
            RelativeFilePath = relativePath;
            FilePath = Path.Combine(RelativeFilePath, FileName);
            ProcessID = processId;
            HysysVersion = hysysVersion;
        }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string RelativeFilePath { get; private set; }
        public int ProcessID { get; private set; }
        public string HysysVersion { get; private set; }

        public void WriteMemoryUsage(object source, EventArgs e)
        {
            using (StreamWriter outputFile = CreateStreamWriter(FilePath, ((Connection)source).Counter != 0))
            {
                PerformanceCounter PC = new PerformanceCounter(".NET CLR Memory", "# Bytes in all heaps", Process.GetProcessById(ProcessID).ProcessName);
                TimeSpan hours = TimeSpan.FromMilliseconds(((Connection)source).Counter * ((Connection)source).Frequency);
                long? totalMemorySize = Process.GetProcessById(ProcessID)?.PrivateMemorySize64;
                if (((Connection)source).Counter == 0)
                {
                    Console.WriteLine($"{HysysVersion}");
                    outputFile.WriteLine($"{HysysVersion}");
                }
                Console.WriteLine($"{hours}, {totalMemorySize}");
                outputFile.WriteLine($"{hours} , {totalMemorySize}");
            }
        }

        private StreamWriter CreateStreamWriter(string filePath, bool AppendText)
        {
            if (AppendText)
            {
                return File.AppendText(filePath);
            }
            else
            {
                return new StreamWriter(filePath);
            }
        }
    }
}
