using Gbs.Shared.Dto.Subjects;

namespace Gbs.Wasm.Store;

public class SubjectStore : BaseStore<SubjectDto, SubjectCreateDto, SubjectCreateDto>, ISubjectStore
{
    public SubjectStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/subjects";

    public override SubjectDto? GetByIdQuery(int id) => Value.FirstOrDefault(x => x.Id == id);
}