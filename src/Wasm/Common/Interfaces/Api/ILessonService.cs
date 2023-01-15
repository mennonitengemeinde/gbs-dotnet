using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Common.Interfaces.Api;

public interface ILessonService : IBaseApiService<LessonResponse>, IApiCrud<CreateLessonRequest, CreateLessonRequest, int>
{
    Task<LessonResponse?> GetById(ComponentBase sender, int id);
    Task UpdateOrder(ComponentBase sender, int id, int order);
    Task UpdateWatched(ComponentBase sender, int id, bool complete);
}