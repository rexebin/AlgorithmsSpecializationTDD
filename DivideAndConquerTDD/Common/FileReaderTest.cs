using NUnit.Framework;

namespace DivideAndConquerTDD.Common
{
    public class FileReaderTest
    {
        [Test]
        public void GivenFolderAndFileName_ShouldReturnPath()
        {
            var folderName = "Common";
            var fileName = "test.txt";
            var sut = new FileReader();
            var result = sut.GetPath(folderName, fileName);
            Assert.True(result.EndsWith($"\\{folderName}\\{fileName}"));
        }

        [Test]
        public void GivenFolderNameAndFileName_ShouldReadFile()
        {
            var folderName = "Common";
            var fileName = "kargerMinCut.txt";
            var sut = new FileReader();
            var file = sut.ReadFile(folderName, fileName);
            Assert.AreEqual(200, file.Length);
        }
    }
}