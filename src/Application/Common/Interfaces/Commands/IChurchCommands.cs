using Gbs.Application.Churches;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface IChurchCommands : ICrudCommand<ChurchDto, CreateChurchRequest, CreateChurchRequest> { }