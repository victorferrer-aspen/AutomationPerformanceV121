﻿using System;
using System.IO;

namespace Simulators
{
    public class CaseInfo
    {
        private readonly string fileName;
        private readonly string filePath;

        public CaseInfo(string filePath, string fileName )
        {
            this.filePath = filePath;
            this.fileName = fileName;
        }

        public string FileName => fileName;
        public string FilePath => filePath;
        public string FullPath => Path.Combine(filePath, fileName);
    }
}
