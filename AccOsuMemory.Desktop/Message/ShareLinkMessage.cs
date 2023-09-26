using AccOsuMemory.Core.Models.SayoModels;
using AccOsuMemory.Desktop.DTO;
using AccOsuMemory.Desktop.DTO.Sayo;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AccOsuMemory.Desktop.Message;

public class ShareLinkMessage : ValueChangedMessage<string>
{
    public TopLevel? TopLevel;
    
    public ShareLinkMessage(string linkUrl,TopLevel? topLevel) : base(linkUrl)
    {
        TopLevel = topLevel;
    }
}