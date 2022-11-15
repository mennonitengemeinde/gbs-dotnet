namespace Gbs.Application.Common.Interfaces.Commands;

public interface ISubjectCommands
{
    Task<Result<SubjectDto>> Add(SubjectCreateDto request);
}