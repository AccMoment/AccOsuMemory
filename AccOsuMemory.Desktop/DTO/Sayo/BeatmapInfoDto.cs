using System;
using System.Collections.Generic;
using System.ComponentModel;
using AccOsuMemory.Core.Models;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Desktop.DTO.Sayo;

public partial class BeatmapInfoDto : BaseModel
{
    public int ApprovedState { get; set; }
    public string Artist { get; set; }
    public string Title { get; set; }
    public List<MapDetailDataDto> MapDetailData { get; set; }
    public double Bpm { get; set; }
    public string Creator { get; set; }
    public int Genre { get; set; }
    public int Language { get; set; }
    public int Sid { get; set; }
    public string Source { get; set; }
    public long LastUpdate { get; set; }
    
    
    [ObservableProperty] private string? _thumbnailFile;
   
    public string GetThumbnailUrl() => $"https://cdn.sayobot.cn:25225/beatmaps/{Sid}/covers/cover.jpg";
   
    [Description("试听音频")] public string PreviewAudio => $"https://cdn.sayobot.cn:25225/preview/{Sid}.mp3";
    
    [Description("完整版下载")] public string FullDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/full/{Sid}";
    //
    // [Description("无视频版下载")]
    // public string NoVideoDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/novideo/{Sid}";
    //
    // [Description("Mini版下载")] public string MiniDownloadUrl => $"https://dl.sayobot.cn/beatmaps/download/mini/{Sid}";
}

public class MapDetailDataDto
{
    public double AR { get; set; }
    public double CS { get; set; }
    public double HP { get; set; }
    public double OD { get; set; }
    public double Aim { get; set; }
    public int Bid { get; set; }
    public int Circles { get; set; }
    public int Length { get; set; }
    public int MaxCombo { get; set; }
    public int Mode { get; set; }
    public int Sliders { get; set; }
    public int Spinners { get; set; }
    public double Star { get; set; }
    public string VersionName { get; set; }
}