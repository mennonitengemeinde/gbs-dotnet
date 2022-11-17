namespace Gbs.Wasm.Common.Interfaces;

public interface IBaseStore
{
    DateTime LastUpdated { get; }
    protected bool NeedsUpdate();
}