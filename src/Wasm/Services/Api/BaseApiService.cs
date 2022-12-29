using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace Gbs.Wasm.Services.Api;

public abstract class BaseApiService<T> : IBaseApiService<T>
{
    protected readonly IDateTimeService DateTimeService;
    protected readonly IUiService UiService;
    public List<T> Data { get; protected set; } = new();
    public DateTime LastUpdated { get; protected set; } = DateTime.MinValue;
    public ServiceError? Error { get; protected set; }
    public bool IsLoading { get; protected set; }
    public event Action<ComponentBase, T, bool, bool>? OnChange;

    protected BaseApiService(
        IDateTimeService dateTimeService,
        IUiService uiService)
    {
        DateTimeService = dateTimeService;
        UiService = uiService;
    }

    protected void NotifyStateChanged(ComponentBase sender, T data, bool isLoading, bool isError)
    {
        OnChange?.Invoke(sender, data, isLoading, isError);
    }

    public async Task SetError(ComponentBase sender, ServiceError? error = null, bool notify = true)
    {
        if (Error == error) return;
        
        Error = error;

        if (Error != null)
            await UiService.ShowErrorAlert(Error.Message, Error.StatusCode);
        
        if (notify)
            NotifyStateChanged();
    }

    public void SetLoading(ComponentBase sender, bool isLoading, bool notify = true)
    {
        if (isLoading == IsLoading)
            return;
        IsLoading = isLoading;
        
        if (notify)
            NotifyStateChanged();
    }

    public async Task SetState(ComponentBase sender, IEnumerable<T> data, bool isLoading = false, ServiceError? error = null)
    {
        Data = data.ToList();
        await SetError(error, false);
        IsLoading = isLoading;
        LastUpdated = DateTimeService.UtcNow;
        NotifyStateChanged();
    }

    public void ClearError(ComponentBase sender)
    {
        if (Error is null)
            return;

        Error = null;
        NotifyStateChanged(sender, );
    }
}