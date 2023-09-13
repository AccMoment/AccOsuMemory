using AccOsuMemory.Core.Models.SayoModels;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AccOsuMemory.Desktop.Message;

public class DownloadTaskMessage : ValueChangedMessage<BeatMap>
{
    public DownloadTaskMessage(BeatMap value) : base(value)
    {
        
    }
}