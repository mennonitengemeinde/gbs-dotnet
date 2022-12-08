using Gbs.Shared.Teachers;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface ITeacherStore : IStore<TeacherResponse, int, CreateTeacherRequest, UpdateTeacherRequest> { }