namespace Gbs.Wasm.Services.Interfaces;

public interface IBaseApiService<T>
{
    List<T> Data { get; }
    DateTime LastUpdated { get; }
    ServiceError? Error { get; }
    bool IsLoading { get; }
    event Action<ComponentBase, T, bool, bool> OnChange;
    
    Task SetError(ComponentBase sender, ServiceError? error, bool notify);
    void SetLoading(ComponentBase sender, bool isLoading, bool notify);
    Task SetState(ComponentBase sender, IEnumerable<T> data, bool isLoading, ServiceError? error);
    void ClearError(ComponentBase sender);
}