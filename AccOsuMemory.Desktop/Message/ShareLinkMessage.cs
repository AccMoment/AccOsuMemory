using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
using AccOsuMemory.Desktop.DTO.Sayo;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AccOsuMemory.Desktop.Message;

public class ShareLinkMessage : ValueChangedMessage<BeatmapDto>
{
    public TopLevel? TopLevel;
    
    public ShareLinkMessage(BeatmapDto value,TopLevel? topLevel) : base(value)
    {
        TopLevel = topLevel;
    }
}