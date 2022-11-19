namespace Gbs.Wasm.Store;

public abstract class BaseStore<T> : IStore<T>
{
    protected BaseStore(HttpClient http, IDateTimeService dateTime, IUiService uiService)
    {
        Http = http;
        DateTimeService = dateTime;
        UiService = uiService;
        LastUpdated = new DateTime(2000, 1, 1);
    }
    
    private bool _isLoading;
    private List<T> _value = new();

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
        private set
        {
            _isLoading = value;
            if (value)
            {
                OnChange?.Invoke(); 
            }
            else
            {
                OnChangeDebounce();
            }
        }
    }

    public DateTime LastUpdated { get; private set; }

    public List<T> Value
    {
        get => _value;
        private set
        {
            _value = value;
            OnChangeDebounce();
            LastUpdated = DateTimeService.UtcNow;
        }
    }

    public async Task Initialize()
    {
        if (DateTimeService.UtcNow - LastUpdated > TimeSpan.FromMinutes(5) || Value.Count == 0)
        {
            await Fetch();
        }
    }

    public void CacheData(List<T> value)
    {
        Value = value;
    }

    public void ReadError()
    {
        HasError = false;
    }

    public async Task Fetch()
    {
        IsLoading = true;
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        if (result.Success == false || result.Data == null)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            Value = new List<T>();
            IsLoading = false;
            return;
        }

        IsLoading = false;
        Value = result.Data;
    }

    public abstract T? GetByIdQuery(int id);

    public async Task<T?> GetById(int id)
    {
        await Initialize();

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
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await Fetch();
    }
    
    // private method
    // call onChange once if more then 1 change happens within 500 milliseconds
    // this is to prevent multiple renders
    private void OnChangeDebounce()
    {
        _debounceTimer?.Dispose();

        _debounceTimer = new Timer(_ =>
        {
            OnChange?.Invoke();
        }, null, TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(-1));
    }
}

public abstract class BaseStore<T, TCreate, TUpdate> : BaseStore<T>, IStore<T, TCreate, TUpdate>
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
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await Fetch();
    }

    public async Task Update(int id, TUpdate item)
    {
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", item)
            .EnsureSuccess<GenerationDto>();
        if (!result.Success)
        {
            await UiService.ShowErrorAlert(result.Message, result.StatusCode);
            HasError = true;
            ErrorMessage = result.Message;
            return;
        }

        await Fetch();
    }
}