namespace Gbs.Wasm.Store;

public class StudentStore : BaseStore<StudentDto, StudentCreateDto, StudentCreateDto>, IStudentStore
{
    public StudentStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime, uiService) { }
    public override string BaseUrl { get; } = "api/students";
    public override StudentDto? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}