namespace Gbs.Application.Features.Lessons.Interfaces;

public interface ILessonCommands : ICrudCommand<LessonResponse, CreateLessonRequest, UpdateLessonRequest>
{
    Task<Result<LessonResponse>> UpdateOrder(int id, int request);
}