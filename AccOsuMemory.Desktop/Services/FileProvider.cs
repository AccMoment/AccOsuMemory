using System;
using System.IO;

namespace AccOsuMemory.Desktop.Services;

public class FileProvider : IFileProvider
{

    private string dataPath => "data";
    private string tempPath => dataPath + "/temp";
    private string logPath => dataPath + "/log.txt";

    public FileProvider()
    {
        if (!Directory.Exists(dataPath)) Directory.CreateDirectory(dataPath);
        if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
        if (!File.Exists(logPath)) File.Create(logPath).Dispose();
    }

    public string GetTempDirectory() => tempPath;

    public string GetLogTxt() => logPath;
}