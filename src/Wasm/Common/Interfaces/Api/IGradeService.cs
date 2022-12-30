namespace Gbs.Wasm.Common.Interfaces.Api;

public interface IGradeService : IBaseApiService<GradeResponse>, IApiCrud<CreateGradeRequest, CreateGradeRequest, int> { }