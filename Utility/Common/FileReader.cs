using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Utility.Common
{
    public class FileReader
    {
        public string[] ReadFile(string fileName, [CallerFilePath] string path = null!)
        {
            return File.ReadAllLines(Path.GetFullPath(path + $"../../{fileName}"));
        }
    }
}