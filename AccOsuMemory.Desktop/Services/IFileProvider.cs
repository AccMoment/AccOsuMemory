namespace AccOsuMemory.Desktop.Services;

public interface IFileProvider
{
    string GetTempDirectory();
    string GetLogTxt();
}