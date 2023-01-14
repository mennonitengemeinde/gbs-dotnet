using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Services.Api;

public class LessonService : BaseApiCrud<LessonResponse, CreateLessonRequest, CreateLessonRequest, int>, ILessonService
{
    public LessonService(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(dateTimeService, uiService, http) { }
    public override string BaseUrl => "api/lessons";
    
    public async Task<LessonResponse?> GetById(ComponentBase sender, int id)
    {
        await Fetch(sender);
        return Data.FirstOrDefault(c => c.Id == id);
    }

    public async Task UpdateOrder(ComponentBase sender, int id, int order)
    {
        IsLoading = true;
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}/order", order)
            .EnsureSuccess<LessonResponse>();

        if (!result.Success)
            await SetError(sender, new(result.Message, result.Errors, result.StatusCode));

        await ForceFetch(sender);
    }

    public async Task UpdateWatched(ComponentBase sender, int id, bool complete)
    {
        IsLoading = true;
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}/watched", complete)
            .EnsureSuccess<LessonResponse>();
        
        if (!result.Success)
            await SetError(sender, new(result.Message, result.Errors, result.StatusCode));

        await ForceFetch(sender);
    }
}