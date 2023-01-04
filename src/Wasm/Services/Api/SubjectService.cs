namespace Gbs.Wasm.Services.Api;

public class SubjectService : BaseApiCrud<SubjectResponse, CreateSubjectRequest, CreateSubjectRequest, int>,
    ISubjectService
{
    public SubjectService(IDateTimeService dateTimeService, IUiService uiService, HttpClient http) : base(
        dateTimeService, uiService, http) { }

    public override string BaseUrl => "api/subjects";

    public async Task<SubjectResponse?> GetById(ComponentBase sender, int id)
    {
        await Fetch(sender);
        return Data.FirstOrDefault(c => c.Id == id);
    }
}