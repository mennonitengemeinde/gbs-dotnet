using Gbs.Shared.Students;

namespace Gbs.Wasm.Store;

public class StudentStore : BaseStore<StudentResponse, int, CreateStudentRequest, UpdateStudentRequest>, IStudentStore
{
    public StudentStore(HttpClient http, IDateTimeService dateTime, IUiService uiService) : base(http, dateTime, uiService) { }
    public override string BaseUrl { get; } = "api/students";
    public override StudentResponse? GetByIdQuery(int id) => Data.FirstOrDefault(x => x.Id == id);
}