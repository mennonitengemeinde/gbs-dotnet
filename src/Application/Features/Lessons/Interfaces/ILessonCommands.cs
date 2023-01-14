namespace Gbs.Application.Features.Lessons.Interfaces;

public interface ILessonCommands : ICrudCommand<LessonResponse, CreateLessonRequest, CreateLessonRequest>
{
    Task<Result<LessonResponse>> UpdateOrder(int id, int request);
    Task<Result<LessonResponse>> UpdateWatched(int id, bool request);
}