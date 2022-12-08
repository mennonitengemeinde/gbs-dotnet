using Gbs.Shared.Subjects;

namespace Gbs.Wasm.Store;

public class SubjectStore : BaseStore<SubjectResponse, int, CreateSubjectRequest, UpdateSubjectRequest>, ISubjectStore
{
    public SubjectStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime,
        uiService) { }

    public override string BaseUrl { get; } = "api/subjects";

    public override SubjectResponse? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}