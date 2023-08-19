﻿using System.Text.Json;
using AccOsuMemory.Core.Models;

namespace AccOsuMemory.Core.Utils;

public static class AppSettingsWriter
{
    private static readonly string SettingsPath = Path.Combine(Environment.CurrentDirectory, "appsettings.json");

    // public static void Write<T>(T appSettings)
    // {
    //     using var writer = new StreamWriter(SettingsPath);
    //     writer.Write(JsonSerializer.Serialize(appSettings, new JsonSerializerOptions
    //     {
    //         WriteIndented = true
    //     }));
    // }

    public static void Write(string? key, object? value)
    {
        var reader = new StreamReader(SettingsPath);
        var settings = JsonSerializer.Deserialize<AppSettings>(reader.ReadToEnd());
        reader.Dispose();
        var property = settings?.GetType().GetProperties().First(f => f.Name == key);
        if (property == null) throw new ArgumentException("Can't not find a correct key");
        if (property.CanWrite) property.SetValue(settings, value);
        using var writer = new StreamWriter(SettingsPath);
        writer.Write(JsonSerializer.Serialize(settings, new JsonSerializerOptions
        {
            WriteIndented = true
        }));
    }
}