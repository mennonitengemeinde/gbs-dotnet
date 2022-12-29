namespace Gbs.Wasm.Services.Api;

public abstract class BaseApiCrud<T, TCreate, TUpdate, TId> : BaseApiService<T>, IApiCrud<TCreate, TUpdate, TId>
{
    protected readonly HttpClient Http;

    protected BaseApiCrud(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(
        dateTimeService, uiService)
    {
        Http = http;
    }

    public abstract string BaseUrl { get; }

    public async Task Fetch(int minutes = 5)
    {
        if (DateTimeService.UtcNow - LastUpdated > TimeSpan.FromMinutes(minutes) || Data.Count == 0)
            await ForceFetch();
    }

    public async Task ForceFetch()
    {
        SetLoading(true);
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        
        if (result.Success == false || result.Data == null)
        {
            await SetState(Array.Empty<T>(), false,
                new ServiceError(result.Message, result.Errors, result.StatusCode));
            return;
        }

        await SetState(result.Data);
    }

    public async Task Create(TCreate request)
    {
        SetLoading(true);
        var result = await Http.PostAsJsonAsync(BaseUrl, request)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await HandleError(result);
            return;
        }

        await ForceFetch();
    }

    public async Task Update(TId id, TUpdate request)
    {
        SetLoading(true);
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", request)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await HandleError(result);
            return;
        }

        await ForceFetch();
    }

    public async Task Delete(TId id)
    {
        SetLoading(true);
        var result = await Http.DeleteAsync($"{BaseUrl}/{id}")
            .EnsureSuccess<bool>();
        if (!result.Success)
        {
            await HandleError(result);
            return;
        }

        await ForceFetch();
    }
    
    protected async Task HandleError(Result result)
    {
        await SetError(new ServiceError(result.Message, result.Errors, result.StatusCode), false);
        SetLoading(false);
    }
}