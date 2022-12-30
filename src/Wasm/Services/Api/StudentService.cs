namespace Gbs.Wasm.Services.Api;

public class StudentService : BaseApiCrud<StudentResponse, CreateStudentRequest, CreateStudentRequest, int>,
    IStudentService
{
    public StudentService(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(
        dateTimeService, uiService, http) { }

    public override string BaseUrl => "api/students";
    
    public async Task<StudentResponse?> GetById(ComponentBase sender, int id)
    {
        await Fetch(sender);
        return Data.FirstOrDefault(c => c.Id == id);
    }
}