using AccOsuMemory.Desktop.ViewModels;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using System;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace AccOsuMemory.Desktop
{
    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            var name = data!.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);

            if (type != null)
            {
                var control = (Control)Activator.CreateInstance(type)!;
                control.DataContext = App.AppHost?.Services.GetService(data.GetType());
                return control;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}