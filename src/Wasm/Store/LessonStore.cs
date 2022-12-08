using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Store;

public class LessonStore : BaseStore<LessonDto, int, LessonCreateDto, LessonCreateDto>, ILessonStore
{
    public LessonStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/lessons";

    public override LessonDto? GetByIdQuery(int id) => Data.FirstOrDefault(l => l.Id == id);

    public async Task UpdateOrder(int id, int order)
    {
        IsLoading = true;
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}/order", order)
            .EnsureSuccess<LessonDto>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            SetErrors(result.Message, result.Errors);
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }
}