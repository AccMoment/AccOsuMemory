using System.Collections.ObjectModel;
using System.Net.Http.Json;
using AccOsuMemory.Core.OsuApi.Sayo.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Mobile.ViewModel;

public partial class MainPageViewModel:ObservableObject
{
    private static readonly HttpClient HttpClient=new();
        
    public string Greeting => "Welcome to Avalonia!";

    [ObservableProperty]
    private ObservableCollection<BeatMap> _beatMaps = new();



    public async void LoadBeatMapsAsync()
    {
        var result = await HttpClient.GetFromJsonAsync<BeatMapList>("https://api.sayobot.cn/beatmaplist?T=2");
        result.BeatMaps.ForEach(map=>BeatMaps.Add(map));
    }
}
