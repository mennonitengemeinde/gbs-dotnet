﻿namespace Gbs.Application.Common.Interfaces.Queries;

public interface IIdentityQueries
{
    Task<Result<List<UserDto>>> GetAll();
    Task<Result<UserDto>> GetById(string id);
    Task<Result<List<RolesDto>>> GetRoles();
}