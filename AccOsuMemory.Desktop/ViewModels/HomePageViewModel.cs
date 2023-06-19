using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AccOsuMemory.Core.OsuApi.Sayo.Model;
using AccOsuMemory.Desktop.Utils;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.ViewModels;

public partial class HomePageViewModel : ViewModelBase
{
    [ObservableProperty] 
    private ObservableCollection<BeatMap> _beatMaps = new();

    [ObservableProperty]
    private bool _canLoadBeatMapList =true;

    private int _currentPage = 1;


    public async Task LoadBeatMapsAsync()
    {
        var result =
            await HttpUtil.HttpClient.GetFromJsonAsync<BeatMapList>(
                $"https://api.sayobot.cn/beatmaplist?T=2&offset={_currentPage}");
        if (result?.Status != 0)
        {
            CanLoadBeatMapList = false;
            return;
        }
        result.BeatMaps.ForEach(map =>
        {
            BeatMaps.Add(map);
        });
        _currentPage++;
    }

    public void AddBeatMap()
    {
        BeatMaps.Add(new BeatMap
        {
            Title = "123",
            Creator = "ab"
        });
    }
}