using System.IO;
using AccOsuMemory.Desktop.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels
{
    public class ViewModelBase : ObservableObject
    {
        internal readonly IFileProvider FileProvider;

        protected ViewModelBase(IFileProvider fileProvider)
        {
            FileProvider = fileProvider;
        }

        public void WriteErrorToFile(string errorText)
        {
            using var file = File.AppendText(FileProvider.GetLogFilePath());
            file.Write(errorText);
        }
    }
}