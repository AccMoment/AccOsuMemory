using System.Text.Json.Serialization;
using AccOsuMemory.Core.Models.OsuModels.V1.Enum;
using AccOsuMemory.Core.Utils.Converter;

namespace AccOsuMemory.Core.Models.OsuModels.V1.Beatmap;

public class BeatMap
{
    [JsonPropertyName("beatmapset_id")] 
    public int BeatMapSetId { get; set; }

    [JsonPropertyName("beatmap_id")] 
    public int BeatMapId { get; set; }

    [JsonPropertyName("approved")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ApprovedState Approved { get; set; }

    [JsonPropertyName("total_length")] 
    public int TotalLength { get; set; }

    [JsonPropertyName("hit_length")] 
    public int HitLength { get; set; }

    [JsonPropertyName("version")] 
    public string Version { get; set; }

    [JsonPropertyName("file_md5")] 
    public string FileMd5 { get; set; }

    [JsonPropertyName("diff_size")] 
    public double DiffSize { get; set; }

    [JsonPropertyName("diff_overall")] 
    public double DiffOverall { get; set; }

    [JsonPropertyName("diff_approach")] 
    public double DiffApproach { get; set; }

    [JsonPropertyName("diff_drain")] 
    public double DiffDrain { get; set; }

    [JsonPropertyName("mode")] 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public GameMode Mode { get; set; }

    [JsonPropertyName("count_normal")] 
    public int CountNormal { get; set; }

    [JsonPropertyName("count_slider")] 
    public int CountSlider { get; set; }

    [JsonPropertyName("count_spinner")] 
    public int CountSpinner { get; set; }

    [JsonPropertyName("submit_date")] 
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime SubmitDate { get; set; }

    [JsonPropertyName("approved_date")] 
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime ApprovedDate { get; set; }

    [JsonPropertyName("last_update")] 
    [JsonConverter(typeof(JsonStringDateTimeConverter))]
    public DateTime LastUpdate { get; set; }

    [JsonPropertyName("artist")] 
    public string Artist { get; set; }

    [JsonPropertyName("artist_unicode")] 
    public string ArtistUnicode { get; set; }

    [JsonPropertyName("title")] 
    public string Title { get; set; }

    [JsonPropertyName("title_unicode")] 
    public string TitleUnicode { get; set; }

    [JsonPropertyName("creator")] 
    public string Creator { get; set; }

    [JsonPropertyName("creator_id")] 
    public int CreatorId { get; set; }

    [JsonPropertyName("bpm")] 
    public double Bpm { get; set; }

    [JsonPropertyName("source")] 
    public string Source { get; set; }

    [JsonPropertyName("tags")] 
    public string Tags { get; set; }

    [JsonPropertyName("genre_id")] 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genre GenreId { get; set; }

    [JsonPropertyName("language_id")] 
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Language LanguageId { get; set; }

    [JsonPropertyName("favourite_count")] 
    public int FavouriteCount { get; set; }

    [JsonPropertyName("rating")] 
    public double Rating { get; set; }

    [JsonPropertyName("storyboard")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool HasStoryBoard { get; set; }

    [JsonPropertyName("video")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool HasVideo { get; set; }

    [JsonPropertyName("download_unavailable")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool DownloadUnavailable { get; set; }

    [JsonPropertyName("audio_unavailable")]
    [JsonConverter(typeof(JsonStringBooleanConverter))]
    public bool AudioUnavailable { get; set; }

    [JsonPropertyName("playcount")] 
    public int Playcount { get; set; }

    [JsonPropertyName("passcount")] 
    public int Passcount { get; set; }

    [JsonPropertyName("packs")] 
    public string Packs { get; set; }

    [JsonPropertyName("max_combo")] 
    public int MaxCombo { get; set; }

    [JsonPropertyName("diff_aim")] 
    public double DiffAim { get; set; }

    [property: JsonPropertyName("diff_speed")]
    public double DiffSpeed { get; set; }

    [JsonPropertyName("difficultyrating")] 
    public double DifficultyRating { get; set; }
}