namespace Gbs.Application.Features.Subjects.Interfaces;

public interface ISubjectCommands
{
    Task<Result<SubjectResponse>> Add(CreateSubjectRequest request);
    Task<Result<SubjectResponse>> Update(int id, UpdateSubjectRequest request);
}