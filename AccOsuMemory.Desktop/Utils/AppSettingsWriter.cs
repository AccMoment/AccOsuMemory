using System;
using System.IO;
using System.Text.Json;
using AccOsuMemory.Core.Models;

namespace AccOsuMemory.Desktop.Utils;

public class AppSettingsWriter
{
    private static string settingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

    public static void Write(AppSettings appSettings)
    {
        using var writer = new StreamWriter(settingsPath);
        writer.Write(JsonSerializer.Serialize(appSettings, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }
    
}