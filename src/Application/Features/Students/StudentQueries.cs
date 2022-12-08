using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Identity.Interfaces;
using Gbs.Application.Features.Students.Interfaces;

namespace Gbs.Application.Features.Students;

public class StudentQueries : IStudentQueries
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityQueries _identityQueries;
    private readonly IAuthenticatedUserService _authUserService;

    public StudentQueries(
        IGbsDbContext context, 
        IMapper mapper, 
        IIdentityQueries identityQueries,
        IAuthenticatedUserService authUserService)
    {
        _context = context;
        _mapper = mapper;
        _identityQueries = identityQueries;
        _authUserService = authUserService;
    }
    
    public async Task<Result<List<StudentResponse>>> GetAll()
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var result = await _context.Students
                .ProjectTo<StudentResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result.Ok(result);
        }

        var user = await _identityQueries.GetById(_authUserService.GetUserId());
        if (user.Data == null)
            return Result.NotFound<List<StudentResponse>>("User not found");
        
        var students = await _context.Students
            .Where(s => s.ChurchId == user.Data.ChurchId)
            .OrderBy(s => s.FirstName)
            .ProjectTo<StudentResponse>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return Result.Ok(students);
    }

    public async Task<Result<StudentResponse>> GetById(int id)
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var student = await _context.Students
                .ProjectTo<StudentResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            return student == null
                ? Result.NotFound<StudentResponse>("Student not found")
                : Result.Ok(student);
        }
        else
        {
            var user = await _identityQueries.GetById(_authUserService.GetUserId());
            if (user.Data == null)
                return Result.NotFound<StudentResponse>("User not found");
            
            var student = await _context.Students
                .ProjectTo<StudentResponse>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id && s.ChurchId == user.Data.ChurchId);
            return student == null
                ? Result.NotFound<StudentResponse>("Student not found")
                : Result.Ok(student);
        }
    }
}