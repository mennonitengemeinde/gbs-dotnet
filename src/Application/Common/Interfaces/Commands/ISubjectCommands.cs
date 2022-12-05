using Gbs.Shared.Subjects;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface ISubjectCommands
{
    Task<Result<SubjectDto>> Add(SubjectCreateDto request);
    Task<Result<SubjectDto>> Update(int id, SubjectCreateDto request);
}