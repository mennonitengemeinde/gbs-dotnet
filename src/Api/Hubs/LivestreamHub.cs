namespace Gbs.Api.Hubs;

public class LivestreamHub : Hub
{
    private readonly IStreamQueries _streamQueries;

    public LivestreamHub(IStreamQueries streamQueries)
    {
        _streamQueries = streamQueries;
    }
    
    public async Task BroadcastActiveStreams()
    {
        var streams = await _streamQueries.GetAllStreams();
        await Clients.All.SendAsync(LiveStreamHubRoutes.ReceiveStreams, streams);
    }
}