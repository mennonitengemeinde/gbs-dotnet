using Gbs.Shared.Students;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface IStudentStore : IStore<StudentResponse, int, CreateStudentRequest, UpdateStudentRequest>
{
    
}