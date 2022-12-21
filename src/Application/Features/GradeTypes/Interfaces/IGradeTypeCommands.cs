using Gbs.Shared.GradeTypes;

namespace Gbs.Application.Features.GradeTypes.Interfaces;

public interface
    IGradeTypeCommands : ICrudCommand<GradeTypeResponse, CreateGradeTypeRequest, CreateGradeTypeRequest> { }