using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
using AccOsuMemory.Desktop.DTO.Sayo;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AccOsuMemory.Desktop.Message;

public class DownloadTaskMessage : ValueChangedMessage<BeatmapDto>
{
    public DownloadTaskMessage(BeatmapDto value) : base(value)
    {
        
    }
}