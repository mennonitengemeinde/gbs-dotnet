namespace Gbs.Wasm.Common.Interfaces.Api;

public interface ISubjectService : IBaseApiService<SubjectResponse>, IApiCrud<CreateSubjectRequest, CreateSubjectRequest, int>
{
    Task<SubjectResponse?> GetById(ComponentBase sender, int id);
}