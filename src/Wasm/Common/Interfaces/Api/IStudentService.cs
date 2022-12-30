namespace Gbs.Wasm.Common.Interfaces.Api;

public interface IStudentService: IBaseApiService<StudentResponse>,
    IApiCrud<CreateStudentRequest, CreateStudentRequest, int>
{
    Task<StudentResponse?> GetById(ComponentBase sender, int id);
}