namespace Gbs.Application.Features.Lessons.Interfaces;

public interface ILessonQueries
{
    Task<Result<List<LessonResponse>>> GetAll(string? visibility);
    Task<Result<LessonResponse>> GetById(int id);
}