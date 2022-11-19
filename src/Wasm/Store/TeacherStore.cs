namespace Gbs.Wasm.Store;

public class TeacherStore : BaseStore<TeacherDto, TeacherCreateDto, TeacherCreateDto>, ITeacherStore
{
    public TeacherStore(
        HttpClient http,
        IDateTimeService dateTime,
        IUiService uiService) : base(http, dateTime, uiService) { }

    public override string BaseUrl { get; } = "api/teachers";
    public override TeacherDto? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}