using Gbs.Shared.Lessons;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface ILessonStore : IStore<LessonDto, int, LessonCreateDto, LessonCreateDto>
{
    Task UpdateOrder(int id, int order);
}