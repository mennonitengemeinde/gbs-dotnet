namespace Gbs.Application.Common.Interfaces.Queries;

public interface ISubjectQueries
{
    Task<Result<List<SubjectDto>>> GetAll();
    Task<Result<SubjectDto>> GetById(int id);
}