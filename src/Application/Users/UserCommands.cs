using Gbs.Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Gbs.Application.Users;

public class UserCommands : IUserCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUserQueries _userQueries;
    private readonly IAuthenticatedUserService _authenticatedUserService;
    private readonly UserManager<User> _userManager;

    public UserCommands(
        IGbsDbContext context, 
        IMapper mapper,
        IUserQueries userQueries,
        IAuthenticatedUserService authenticatedUserService,
        UserManager<User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userQueries = userQueries;
        _authenticatedUserService = authenticatedUserService;
        _userManager = userManager;
    }
    
    public async Task<Result<string>> Add(RegisterDto request)
    {
        if (await _context.Users.AnyAsync(u => u.Email != null && u.Email.ToLower().Equals(request.Email.ToLower())))
        {
            return Result.BadRequest<string>("Email already exists");
        }

        var newUser = _mapper.Map<User>(request);

        var result = await _userManager.CreateAsync(newUser, request.Password);

        if (result.Succeeded)
            return Result.Ok(newUser.Id, "User created successfully");

        var error = result.Errors.Select(x => x.Description).First();
        return Result.BadRequest<string>(error);
    }

    public async Task<Result<UserDto>> UpdateRoles(string id, List<string> newRoles)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserDto>("User not found");

        var userRoles = await _userManager.GetRolesAsync(user);

        var rolesToRemove = userRoles.Except(newRoles).ToList();
        if (rolesToRemove.Any())
        {
            var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
            if (!removeResult.Succeeded)
                return Result.BadRequest<UserDto>("Failed to remove roles");
        }

        var rolesToAdd = newRoles.Except(userRoles).ToList();
        if (!rolesToAdd.Any()) 
            return await _userQueries.GetById(user.Id);
        
        var result = await _userManager.AddToRolesAsync(user, rolesToAdd);
        if (!result.Succeeded) 
            return Result.BadRequest<UserDto>("Failed to add roles");
        
        return await _userQueries.GetById(user.Id);
    }

    public async Task<Result<UserDto>> UpdateActiveState(string id, bool request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.BadRequest<UserDto>("User not found");

        user.IsActive = request;
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<UserDto>(user));
    }

    public async Task<Result<UserDto>> UpdateChurch(string id, UserUpdateChurchDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<UserDto>("User not found");

        user.ChurchId = request.ChurchId;
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<UserDto>(user));
    }

    public async Task<Result<bool>> Delete(string id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return Result.NotFound<bool>("User not found");
        
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return Result.Ok(true);
    }
}