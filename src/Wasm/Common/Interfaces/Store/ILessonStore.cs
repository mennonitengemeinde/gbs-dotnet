using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface ILessonStore : IStore<LessonResponse, int, CreateLessonRequest, UpdateLessonRequest>
{
    Task UpdateOrder(int id, int order);
}