using Gbs.Application.Students;
using Gbs.Shared.Students;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface IStudentCommands : ICrudCommand<StudentDto, StudentCreateDto, StudentCreateDto>
{
    
}