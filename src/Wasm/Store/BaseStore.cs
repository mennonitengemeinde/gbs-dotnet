namespace Gbs.Wasm.Store;

public abstract class BaseStore<T, TId> : IStore<T, TId>
{
    protected BaseStore(HttpClient http, IDateTimeService dateTime, IUiService uiService)
    {
        Http = http;
        DateTimeService = dateTime;
        UiService = uiService;
        LastUpdated = new DateTime(2000, 1, 1);
    }
    
    private bool _isLoading;
    private List<T> _data = new();

    protected readonly HttpClient Http;
    protected readonly IDateTimeService DateTimeService;
    protected readonly IUiService UiService;
    private Timer? _debounceTimer;

    public event Action? OnChange;
    public abstract string BaseUrl { get; }
    public bool HasError { get; protected set; }
    public string? ErrorMessage { get; protected set; }

    public bool IsLoading
    {
        get => _isLoading;
        protected set
        {
            if (value == _isLoading && value) return;
            
            if (value)
            {
                OnChange?.Invoke(); 
            }
            else
            {
                OnChangeDebounce();
            }
            _isLoading = value;
        }
    }

    public DateTime LastUpdated { get; private set; }

    public List<T> Data
    {
        get => _data;
        private set
        {
            _data = value;
            OnChangeDebounce();
            LastUpdated = DateTimeService.UtcNow;
        }
    }

    public async Task Fetch()
    {
        if (DateTimeService.UtcNow - LastUpdated > TimeSpan.FromMinutes(5) || Data.Count == 0)
        {
            await ForceFetch();
        }
    }

    public void CacheData(List<T> value)
    {
        Data = value;
    }

    public void ClearErrors()
    {
        HasError = false;
    }

    public async Task ForceFetch()
    {
        IsLoading = true;
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        if (result.Success == false || result.Data == null)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            Data = new List<T>();
            IsLoading = false;
            return;
        }

        IsLoading = false;
        Data = result.Data;
    }

    public abstract T? GetByIdQuery(TId id);

    public async Task<T?> GetById(TId id)
    {
        await Fetch();

        var result = GetByIdQuery(id);
        if (result == null)
        {
            await UiService.ShowErrorAlert("Could not find item with id " + id);
        }

        return result;
    }

    public async Task Delete(TId id)
    {
        IsLoading = true;
        var result = await Http.DeleteAsync($"{BaseUrl}/{id}")
            .EnsureSuccess<bool>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }
    
    private void OnChangeDebounce()
    {
        _debounceTimer?.Dispose();

        _debounceTimer = new Timer(_ =>
        {
            OnChange?.Invoke();
        }, null, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(-1));
    }
}

public abstract class BaseStore<T, TId, TCreate, TUpdate> : BaseStore<T, TId>, IStore<T, TId, TCreate, TUpdate>
{
    protected BaseStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public virtual async Task Add(TCreate item)
    {
        IsLoading = true;
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }

    public async Task Update(TId id, TUpdate item)
    {
        IsLoading = true;
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }
}