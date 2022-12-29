namespace Gbs.Wasm.Services.Interfaces;

public interface IGradeService : IBaseApiService<GradeResponse>, IApiCrud<CreateGradeRequest, CreateGradeRequest, int> { }