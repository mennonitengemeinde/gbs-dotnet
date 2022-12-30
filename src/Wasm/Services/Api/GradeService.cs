namespace Gbs.Wasm.Services.Api;

public class GradeService : BaseApiCrud<GradeResponse, CreateGradeRequest, CreateGradeRequest, int>, IGradeService
{
    public GradeService(IDateTimeService dateTimeService, HttpClient http, IUiService uiService) : base(dateTimeService,
        uiService, http) { }

    public override string BaseUrl => "api/grades";
}