using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gbs.Wasm.Shared.Cascading;

public abstract class BaseCascadingState<T, TCreate, TIdType, TUpdate> : ComponentBase, IApiState
{
    [Parameter] public RenderFragment? ChildContent { get; set; }

    [Inject] protected HttpClient Http { get; set; } = null!;
    [Inject] protected IDateTimeService DateTimeService { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] protected NavigationManager NavigationManager { get; set; } = null!;

    private List<T> _data = new();
    private bool _isLoading;
    private string[]? _errors;
    private string? _errorMessage;
    private bool _hasError;
    private DateTime _lastUpdated = DateTime.MinValue;

    public abstract string BaseUrl { get; }

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

    public string? ErrorMessage
    {
        get => _errorMessage;
        protected set
        {
            if (_errorMessage == value) return;
            _errorMessage = value;
            // StateHasChanged();
        }
    }

    public string[]? Errors
    {
        get => _errors;
        protected set
        {
            if (_errors == value) return;
            _errors = value;
            // StateHasChanged();
        }
    }

    public bool HasError
    {
        get => _hasError;
        protected set
        {
            if (_hasError == value) return;
            _hasError = value;
            // StateHasChanged();
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set
        {
            if (_isLoading == value) return;
            _isLoading = value;
            // StateHasChanged();
        }
    }

    public async Task Fetch()
    {
        if (DateTimeService.UtcNow - _lastUpdated > TimeSpan.FromMinutes(5) || Data.Count == 0)
        {
            await ForceFetch();
        }
    }

    public async Task ForceFetch()
    {
        IsLoading = true;
        var result = await Http.GetAsync(BaseUrl)
            .EnsureSuccess<List<T>>();
        if (result.Success == false || result.Data == null)
        {
            await SetErrors(result.Message, result.Errors, result.StatusCode);
            Data = new List<T>();
            IsLoading = false;
            return;
        }

        IsLoading = false;
        Data = result.Data;
    }

    public void ClearErrors()
    {
        _hasError = true;
        _errorMessage = null;
        _errors = null;
    }

    protected async Task SetErrors(string message, string[]? errors, int statusCode = 400)
    {
        _hasError = true;
        _errorMessage = message;
        _errors = errors;
        if (statusCode == 401)
        {
            await LocalStorage.RemoveItemAsync("authToken");
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            NavigationManager.NavigateTo("");
        }
    }

    public virtual async Task Add(TCreate item)
    {
        IsLoading = true;
        var result = await Http.PostAsJsonAsync(BaseUrl, item)
            .EnsureSuccess<T>();
        if (!result.Success)
        {
            await SetErrors(result.Message, result.Errors, result.StatusCode);
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
            await SetErrors(result.Message, result.Errors, result.StatusCode);
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
            await SetErrors(result.Message, result.Errors, result.StatusCode);
            IsLoading = false;
            return;
        }

        await ForceFetch();
    }
}