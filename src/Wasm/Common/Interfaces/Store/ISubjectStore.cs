using Gbs.Shared.Subjects;

namespace Gbs.Wasm.Common.Interfaces.Store;

public interface ISubjectStore : IStore<SubjectResponse, int, CreateSubjectRequest, UpdateSubjectRequest> { }