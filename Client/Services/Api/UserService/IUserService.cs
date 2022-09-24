﻿namespace gbs.Client.Services.Api.UserService;

public interface IUserService
{
    List<UserDto> Users { get; set; }
    event Action UsersChanged;
    Task GetUsers();
    Task<ServiceResponse<List<UserDto>>> FetchUsers();
    Task UpdateChurch(int userId, UserUpdateChurchDto updateDto);
    Task UpdateRole(int userId, UserUpdateRoleDto userUpdateRoleDto);
    Task UpdateActiveState(int userId, UserUpdateActiveStateDto userUpdateActiveDto);
}