using System.ComponentModel;
using System.Text.Json.Serialization;
using AccOsuMemory.Core.JsonConverter;
using AccOsuMemory.Core.Models.SayoModels.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AccOsuMemory.Core.Models.SayoModels;

public class BeatmapListInfo
{
    [JsonPropertyName("data")] public BeatmapInfo BeatmapInfo { get; set; }

    [JsonPropertyName("status")]
    [Description("请求状态标识")]
    public int Status { get; set; }
}

public class BeatmapInfo
{
    [JsonPropertyName("approved")]
    [Description("Rank状态")]
    public int ApprovedState { get; set; }

    [JsonPropertyName("approved_date")]
    // [JsonConverter(typeof(JsonTimestampDateTimeConverter))]
    [Description("Ranked时间")]
    public long ApprovedDate { get; set; }

    [JsonPropertyName("artist")]
    [Description("艺术家")]
    public string Artist { get; set; } = string.Empty;


    [JsonPropertyName("artistU")]
    [Description("艺术家 Unicode")]
    public string ArtistU { get; set; } = string.Empty;


    [JsonPropertyName("bid_data")]
    [Description("谱面详细数据")]
    public List<MapDetailData> MapDetailData { get; set; }


    [JsonPropertyName("bids_amount")]
    [Description("此谱面集合中的谱面数量")]
    public int MapAmount { get; set; }


    [JsonPropertyName("bpm")] public double Bpm { get; set; }

    [JsonPropertyName("creator")]
    [Description("作图者")]
    public string Creator { get; set; } = string.Empty;

    [JsonPropertyName("creator_id")]
    [Description("作图者的id")]
    public int CreatorId { get; set; }

    [JsonPropertyName("favourite_count")]
    [Description("收藏数")]
    public int FavouriteCount { get; set; }

    [JsonPropertyName("genre")]
    [Description("风格")]
    public int Genre { get; set; }

    [JsonPropertyName("language")]
    [Description("语言")]
    public int Language { get; set; }

    [JsonPropertyName("last_update")]
    // [JsonConverter(typeof(JsonTimestampDateTimeConverter))]
    [Description("谱面最后更新时间")]
    public long LastUpdate { get; set; }

    [JsonPropertyName("local_update")]
    // [JsonConverter(typeof(JsonTimestampDateTimeConverter))]
    [Description("最后检查更新时间")]
    public long LocalUpdate { get; set; }

    [JsonPropertyName("preview")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    [Description("是否有预览")]
    public bool Preview { get; set; }

    [JsonPropertyName("sid")]
    [Description("谱面集合id")]
    public int Sid { get; set; }

    [JsonPropertyName("source")]
    [Description("提供方/专辑/……")]
    public string Source { get; set; } = string.Empty;

    [JsonPropertyName("storyboard")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    [Description("是否有StoryBoard")]
    public bool StoryBoard { get; set; }

    [JsonPropertyName("tags")]
    [Description("标签")]
    public string Tags { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    [Description("标题")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("titleU")]
    [Description("标题 Unicode")]
    public string TitleU { get; set; } = string.Empty;

    [JsonPropertyName("video")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    [Description("是否有视频")]
    public bool Video { get; set; }

    // [Description("缩略图")] [ObservableProperty]
    // private string? _thumbnailFile;
    //
    // public string GetThumbnailUrl() => $"https://cdn.sayobot.cn:25225/beatmaps/{Sid}/covers/cover.jpg";
    
    
}

public class MapDetailData
{
    [JsonPropertyName("AR")]
    [Description("缩圈速度")]
    public double AR { get; set; }

    [JsonPropertyName("CS")]
    [Description("缩圈速度")]
    public double CS { get; set; }

    [JsonPropertyName("HP")]
    [Description("缩圈血量")]
    public double HP { get; set; }

    [JsonPropertyName("OD")]
    [Description("缩圈准度")]
    public double OD { get; set; }

    [JsonPropertyName("aim")]
    [Description("移动难度")]
    public double Aim { get; set; }

    [JsonPropertyName("audio")]
    [Description("音频的文件名")]
    public string AudioName { get; set; } = string.Empty;

    [JsonPropertyName("bg")]
    [Description("背景图的文件名")]
    public string Background { get; set; } = string.Empty;

    [JsonPropertyName("bid")]
    [Description("谱面唯一id")]
    public int Bid { get; set; }

    [JsonPropertyName("circles")]
    [Description("泡泡数量")]
    public int Circles { get; set; }

    [JsonPropertyName("hit300window")]
    [Description("在这个时间内（ms）可以打出300分数")]
    public int hit300window { get; set; }

    [JsonPropertyName("img")]
    [Description("背景图的MD5(已废弃)")]
    public string Img { get; set; } = string.Empty;

    [JsonPropertyName("length")]
    [Description("长度（秒）")]
    public int Length { get; set; }

    [JsonPropertyName("maxcombo")]
    [Description("最大连击")]
    public int MaxCombo { get; set; }

    [JsonPropertyName("mode")]
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    [Description("游戏模式")]
    public int Mode { get; set; }

    [JsonPropertyName("passcount")]
    [Description("游玩次数")]
    public int PassCount { get; set; }

    [JsonPropertyName("playcount")]
    [Description("通过次数")]
    public int PlayCount { get; set; }

    [JsonPropertyName("pp")]
    [Description("该谱面的最大PP")]
    public double PP { get; set; }

    [JsonPropertyName("pp_acc")]
    [Description("该铺面的ACC PP")]
    public double AccPP { get; set; }

    [JsonPropertyName("pp_aim")]
    [Description("该谱面的移动PP")]
    public double AimPP { get; set; }

    [JsonPropertyName("pp_speed")]
    [Description("该铺面的手速PP")]
    public double SpeedPP { get; set; }

    [JsonPropertyName("sliders")]
    [Description("滑条数量")]
    public int Sliders { get; set; }

    [JsonPropertyName("speed")]
    [Description("手速难度")]
    public double Speed { get; set; }

    [JsonPropertyName("spinners")]
    [Description("转盘数量")]
    public int Spinners { get; set; }

    [JsonPropertyName("star")]
    [Description("总体难度")]
    public double Star { get; set; }

    [JsonPropertyName("strain_aim")]
    [Description("移动难度曲线")]
    public string AimStrain { get; set; } = string.Empty;

    [JsonPropertyName("strain_speed")]
    [Description("手速难度曲线")]
    public string SpeedStrain { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    [Description("难度名")]
    public string VersionName { get; set; } = string.Empty;
}