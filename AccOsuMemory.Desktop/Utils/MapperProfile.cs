using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
using AccOsuMemory.Desktop.DTO.Sayo;
using AutoMapper;

namespace AccOsuMemory.Desktop.Utils;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Beatmap, BeatmapDto>();
        CreateMap<BeatmapInfo, BeatmapInfoDto>();
        CreateMap<BeatmapInfoDto, BeatmapDto>();
        CreateMap<MapDetailData, MapDetailDataDto>();
    }
}