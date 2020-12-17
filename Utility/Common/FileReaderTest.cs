using NUnit.Framework;

namespace Utility.Common
{
    public class FileReaderTest
    {
        [Test]
        public void GivenFolderNameAndFileName_ShouldReadFile()
        {
            var folderName = "Common";
            var fileName = "kargerMinCut.txt";
            var sut = new FileReader();
            var file = sut.ReadFile(fileName);
            Assert.AreEqual(200, file.Length);
        }
    }
}