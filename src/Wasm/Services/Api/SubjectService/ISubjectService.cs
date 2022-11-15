using Gbs.Shared.Dto.Subjects;

namespace Gbs.Wasm.Services.Api.SubjectService;

public interface ISubjectService
{
    List<SubjectDto> Subjects { get; set; }
    event Action SubjectsChanged;
    Task FetchSubjects();
    Task<Result<SubjectDto>> FetchSubject(int id);
    Task AddSubject(SubjectCreateDto subject);
}