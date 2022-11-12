namespace Gbs.Api.Hubs;

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
        await Clients.All.SendAsync(LiveStreamHubRoutes.ReceiveStreams, streams);
    }
}