using System.IO.Compression;
namespace Lab08_02
{
    class Program
    {
        static void Main(string[] args) {
            Console.Write($"Enter the path to catalog: ");
            string catalog = Console.ReadLine();
            Console.Write($"Enter the name of the file to find: ");
            string fileName = Console.ReadLine();
            foreach (string foundFile in Directory.EnumerateFiles(
                catalog,
                fileName,
                SearchOption.AllDirectories)
                )
            {
                FileInfo fileInfo = new FileInfo(foundFile);
                try
                {
                    Console.WriteLine(
                        $"Found file: {fileInfo.FullName}\n" +
                        $"Size: {fileInfo.Length} bytes\n" +
                        $"Created: {fileInfo.CreationTime}\n" +
                        $"Content: ");
                    using (FileStream fileStream = fileInfo.OpenRead())
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            Console.WriteLine(streamReader.ReadToEnd());
                        }
                    }





                    void CompressFile(string sourceFile, string destinationFile)
                    {
                        using (FileStream sourceStream = new(sourceFile, FileMode.Open, FileAccess.Read))
                        using (FileStream destinationStream = new(destinationFile, FileMode.Create, FileAccess.Write))
                        using (GZipStream compressionStream = new(destinationStream, CompressionMode.Compress))
                        {
                            sourceStream.CopyTo(compressionStream);
                        }
                    }



                    string startPath = fileInfo.FullName;
                    string endPath = fileInfo.FullName + ".gz";
                    CompressFile(startPath, endPath);
                    FileInfo compressedFileInfo = new FileInfo(endPath);
                    Console.WriteLine($"{fileInfo.Name} compressed succesfully");
                    Console.WriteLine($"First size: {fileInfo.Length} bytes Compressed size: {compressedFileInfo.Length} bytes");
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}