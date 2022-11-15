using Gbs.Shared.Dto.Subjects;

namespace Gbs.Wasm.Services.Api.SubjectService;

public class SubjectService : ISubjectService
{
    public List<SubjectDto> Subjects { get; set; }
    public event Action? SubjectsChanged;
    public async Task FetchSubjects()
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SubjectDto>> FetchSubject(int id)
    {
        throw new NotImplementedException();
    }

    public async Task AddSubject(SubjectCreateDto subject)
    {
        throw new NotImplementedException();
    }
}