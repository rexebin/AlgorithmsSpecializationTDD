using System;
using System.IO;

namespace Utility.Common
{
    public class FileReader
    {
        public string GetPath(string folderName, string fileName)
        {
            return Path.GetFullPath(@$"../../../{folderName}/{fileName}", Environment.CurrentDirectory);
        }

        public string[] ReadFile(string folderName, string fileName)
        {
            return File.ReadAllLines(GetPath(folderName, fileName));
        }
    }
}