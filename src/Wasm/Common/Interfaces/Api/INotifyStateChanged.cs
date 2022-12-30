namespace Gbs.Wasm.Common.Interfaces.Api;

public interface INotifyStateChanged<T>
{
    void NotifyStateChanged(ComponentBase sender, List<T>? data, bool isLoading, ServiceError? error);
}