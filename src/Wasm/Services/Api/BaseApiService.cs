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
    public event Action<ComponentBase, List<T>?, bool, ServiceError?>? OnChange;

    protected BaseApiService(
        IDateTimeService dateTimeService,
        IUiService uiService)
    {
        DateTimeService = dateTimeService;
        UiService = uiService;
    }

    protected void NotifyStateChanged(ComponentBase sender, List<T>? data, bool isLoading, ServiceError? error)
    {
        OnChange?.Invoke(sender, data, isLoading, error);
    }

    public async Task SetError(ComponentBase sender, ServiceError? error = null, bool notify = true)
    {
        if (Error == error) return;
        
        Error = error;

        if (Error != null)
            await UiService.ShowErrorAlert(Error.Message, Error.StatusCode);
        
        if (notify)
            NotifyStateChanged(sender, null, IsLoading, error);
    }

    public void SetLoading(ComponentBase sender, bool isLoading, bool notify = true)
    {
        if (isLoading == IsLoading)
            return;
        IsLoading = isLoading;
        
        if (notify)
            NotifyStateChanged(sender, null, isLoading, null);
    }

    public async Task SetState(ComponentBase sender, List<T> data, bool isLoading = false, ServiceError? error = null)
    {
        Data = data;
        await SetError(sender, error, false);
        IsLoading = isLoading;
        LastUpdated = DateTimeService.UtcNow;
        NotifyStateChanged(sender, data, isLoading, error);
    }

    public void ClearError(ComponentBase sender)
    {
        if (Error is null)
            return;

        Error = null;
        NotifyStateChanged(sender, null, IsLoading, null);
    }
}