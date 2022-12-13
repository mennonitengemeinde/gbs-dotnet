using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using ErrorModel = Gbs.Wasm.Common.Models.Error;

namespace Gbs.Wasm.Shared.Cascading;

public abstract class BaseCascadingState<T, TCreate, TIdType, TUpdate> : ComponentBase, IApiState
{
    private List<T> _data = new();

    private ErrorModel? _error;
    private bool _isLoading;

    private DateTime _lastUpdated = DateTime.MinValue;

    [Inject] protected IUiService UiService { get; set; } = null!;
    [Inject] protected HttpClient Http { get; set; } = null!;
    [Inject] protected IDateTimeService DateTimeService { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    public List<T> Data
    {
        get => _data;
        set
        {
            _data = value;
            _lastUpdated = DateTimeService.UtcNow;
            // StateHasChanged();
        }
    }

    public ErrorModel? Error
    {
        get => _error;
        private set
        {
            if (value?.Errors == _error?.Errors
                && value?.Message == _error?.Message
                && value?.StatusCode == _error?.StatusCode) return;
            _error = value;
        }
        // StateHasChanged();
    }

    public bool IsLoading
    {
        get => _isLoading;
        protected set
        {
            if (_isLoading == value) return;
            _isLoading = value;
            // StateHasChanged();
        }
    }

    public abstract string BaseUrl { get; }

    public async Task Fetch()
    {
        if (DateTimeService.UtcNow - _lastUpdated > TimeSpan.FromMinutes(5) || Data.Count == 0) await ForceFetch();
    }

    public async Task ForceFetch()
    {
        IsLoading = true;
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        if (result.Success == false || result.Data == null)
        {
            await SetError(result.Message, result.Errors, result.StatusCode);
            Data = new List<T>();
            IsLoading = false;
            return;
        }

        IsLoading = false;
        Data = result.Data;
    }

    public void ClearError()
    {
        Error = null;
    }

    protected async Task SetError(string message, string[]? errors, int statusCode = 400)
    {
        Error = new ErrorModel
        {
            Errors = errors,
            Message = message,
            StatusCode = statusCode
        };

        if (_error != null)
            UiService.ShowErrorAlert(_error.Message, _error.StatusCode);

        if (statusCode == 401)
        {
            await LocalStorage.RemoveItemAsync("authToken");
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("");
        }
    }

    public virtual async Task Create(TCreate item)
    {
        IsLoading = true;
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await SetError(result.Message, result.Errors, result.StatusCode);
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }

    public async Task Update(TIdType id, TUpdate item)
    {
        IsLoading = true;
        var result = await Http.PutAsJsonAsync($"{BaseUrl}/{id}", item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await SetError(result.Message, result.Errors, result.StatusCode);
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }

    public async Task Delete(TIdType id)
    {
        IsLoading = true;
        var result = await Http.DeleteAsync($"{BaseUrl}/{id}")
            .EnsureSuccess<bool>();
        if (!result.Success)
        {
            await SetError(result.Message, result.Errors, result.StatusCode);
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }
}