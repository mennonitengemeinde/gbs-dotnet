namespace Gbs.Wasm.Services.Api;

public interface IGradeService
{   
    ServiceError? Error { get; }
    event Action OnChange;
    void NotifyStateChanged();
    Task Create(CreateGradeRequest request);
    Task Update(CreateGradeRequest request);
    Task Delete(int id);
    
    void ClearError();
}