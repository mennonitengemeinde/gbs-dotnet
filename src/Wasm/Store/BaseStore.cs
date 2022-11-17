namespace Gbs.Wasm.Store;

public abstract class BaseStore<T> : IStore<T>
{
    protected List<T> Values = new();
    protected readonly HttpClient Http;
    protected readonly IDateTimeService DateTimeService;
    protected readonly IUiService UiService;
    public abstract string BaseUrl { get; }

    protected BaseStore(HttpClient http, IDateTimeService dateTime, IUiService uiService)
    {
        Http = http;
        DateTimeService = dateTime;
        UiService = uiService;
        LastUpdated = new DateTime(2000, 1, 1);
    }
    
    public abstract event Action? OnChange;
    public abstract List<T> Value { get; protected set; }
    public DateTime LastUpdated { get; protected set; }

    public async Task Fetch()
    {
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        if (result.Success == false || result.Data == null)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            Value = new List<T>();
            return;
        }

        Value = result.Data;
    }
    public abstract T? GetByIdQuery(int id);

    public async Task<T?> GetById(int id)
    {
        if (DateTimeService.UtcNow - LastUpdated > TimeSpan.FromMinutes(5) || Value.Count == 0)
        {
            await Fetch();
        }

        var result = GetByIdQuery(id);
        if (result == null)
        {
            await UiService.ShowErrorAlert("Could not find item with id " + id);
        }

        return result;
    }

    public async Task Delete(int id)
    {
        var result = await Http.DeleteAsync($"{BaseUrl}/{id}")
            .EnsureSuccess<bool>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            return;
        }

        await Fetch();
    }
}

public abstract class BaseStore<T, TCreate> : BaseStore<T>, IStore<T, TCreate>
{
    protected BaseStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public async Task Add(TCreate item)
    {
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            return;
        }

        await Fetch();
    }

    public async Task Update(int id, TCreate item)
    {
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", item)
            .EnsureSuccess<GenerationDto>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            return;
        }

        await Fetch();
    }
}