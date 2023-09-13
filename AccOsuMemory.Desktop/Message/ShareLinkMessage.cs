using AccOsuMemory.Core.Models.SayoModels;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AccOsuMemory.Desktop.Message;

public class ShareLinkMessage : ValueChangedMessage<BeatMap>
{
    public TopLevel? TopLevel;
    
    public ShareLinkMessage(BeatMap value,TopLevel? topLevel) : base(value)
    {
        TopLevel = topLevel;
    }
}