using System;
using System.IO;

namespace POCOs
{
    public class CaseInfo
    {
        private readonly string fileName;
        private readonly string filePath;

        public CaseInfo(string fileName, string filePath)
        {
            this.fileName = fileName;
            this.filePath = filePath;
        }

        public string FileName => fileName;

        public string FilePath => filePath;
        public string FullPath => Path.Combine(filePath, fileName);
    }
}
