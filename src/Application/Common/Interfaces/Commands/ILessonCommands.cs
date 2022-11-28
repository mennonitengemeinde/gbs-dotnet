using Gbs.Application.Lessons;
using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface ILessonCommands : ICrudCommand<LessonDto, LessonCreateDto, LessonCreateDto>
{
    Task<Result<LessonDto>> UpdateOrder(int id, int request);
}