﻿using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using AccOsuMemory.Core.Models.SayoModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Mobile.ViewModel;

public partial class MainPageViewModel:ObservableObject
{
    private static readonly HttpClient HttpClient=new();
        
    public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    private ObservableCollection<Beatmap> _beatMaps = new();



    public async void LoadBeatMapsAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<BeatmapList>("https://api.sayobot.cn/beatmaplist?T=2");
        result.BeatMaps.ForEach(map=>BeatMaps.Add(map));
    }
}
