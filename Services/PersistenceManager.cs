using Simulators;
using System;
using System.Diagnostics;
using System.IO;

namespace Services
{
    public class PersistenceManager
    {
        public PersistenceManager(string filePath, SimulatorInfo simInfo)
        {
            FileName = $"PerformanceMonitor{simInfo.ShortVersion}.csv";
            ProcessInstanceName = GetProcessInstanceName(simInfo.ProcessId);
            RelativeFilePath = filePath;
            FilePath = Path.Combine(RelativeFilePath, FileName);
            SimInfo = simInfo;
        }
        public string FileName { get; private set; }
        public string FilePath { get; private set; }
        public string RelativeFilePath { get; private set; }
        public string ProcessInstanceName { get; private set; }
        public SimulatorInfo SimInfo { get; private set; }
        private string GetProcessInstanceName(int pid)
        {
            PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

            string[] instances = cat.GetInstanceNames();
            foreach (string instance in instances)
            {

                using (PerformanceCounter cnt = new PerformanceCounter("Process",
                     "ID Process", instance, true))
                {
                    int val = (int)cnt.RawValue;
                    if (val == pid)
                    {
                        return instance;
                    }
                }
            }
            throw new Exception("Could not find performance counter " +
                "instance name for current process. This is truly strange ...");
        }
        public void WriteMemoryUsage(object source, ConnectionEventArgs e)
        {
            using (StreamWriter outputFile = CreateStreamWriter(FilePath, e.Counter != 0))
            {
                //PerformanceCounter privateBytes = new PerformanceCounter("Process", "Private Bytes", ProcessInstanceName);
                string processName = Process.GetProcessById(SimInfo.ProcessId).ProcessName;
                PerformanceCounter bytesInAllHeaps = new PerformanceCounter(".NET CLR Memory", "# Gen 2 Collections", processName);

                TimeSpan hours = TimeSpan.FromMilliseconds(e.Counter * e.Frequency);
                long? totalMemorySize = Process.GetProcessById(SimInfo.ProcessId)?.PrivateMemorySize64;
                float? privateBytesSize = 0;// privateBytes.NextValue();
                float? bytesInAllHeapsSize = bytesInAllHeaps.NextValue();

                if (e.Counter == 0)
                {
                    Console.WriteLine($"{SimInfo.LongVersion}");
                    outputFile.WriteLine($"{SimInfo.LongVersion}");
                }
                Console.WriteLine($"{hours}, {processName}, {ProcessInstanceName} {totalMemorySize}, {bytesInAllHeapsSize}");
                outputFile.WriteLine($"{hours} , {totalMemorySize}, {bytesInAllHeapsSize}, {totalMemorySize - bytesInAllHeapsSize}");
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
