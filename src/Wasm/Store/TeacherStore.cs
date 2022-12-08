using Gbs.Shared.Teachers;

namespace Gbs.Wasm.Store;

public class TeacherStore : BaseStore<TeacherResponse, int, CreateTeacherRequest, UpdateTeacherRequest>, ITeacherStore
{
    public TeacherStore(
        HttpClient http,
        IDateTimeService dateTime,
        IUiService uiService) : base(http, dateTime, uiService) { }

    public override string BaseUrl { get; } = "api/teachers";
    public override TeacherResponse? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}