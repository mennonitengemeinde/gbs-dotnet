﻿using Gbs.Application.Students;

namespace Gbs.Application.Common.Interfaces.Commands;

public interface IStudentCommands : ICrudCommand<StudentDto, StudentCreateDto, StudentCreateDto>
{
    
}