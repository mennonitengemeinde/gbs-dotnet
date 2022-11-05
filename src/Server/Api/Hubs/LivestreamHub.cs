using Gbs.Server.Application.Common.Interfaces.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace gbs.Server.Hubs;

public class LivestreamHub : Hub
{
    private readonly IStreamRepository _streamRepo;

    public LivestreamHub(IStreamRepository streamRepo)
    {
        _streamRepo = streamRepo;
    }
    
    public async Task BroadcastActiveStreams()
    {
        var streams = await _streamRepo.GetLiveStreams();
        await Clients.All.SendAsync("ReceiveStreams", streams);
    }
}