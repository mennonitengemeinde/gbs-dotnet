using Gbs.Application.Common.Interfaces.Services;
using Gbs.Domain.Common.Wrapper;

namespace Gbs.Application.Students;

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
    
    public async Task<Result<List<StudentDto>>> GetAll()
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var result = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            return Result.Ok(result);
        }

        var user = await _identityQueries.GetById(_authUserService.GetUserId());
        if (user.Data == null)
            return Result.NotFound<List<StudentDto>>("User not found");
        
        var students = await _context.Students
            .Where(s => s.ChurchId == user.Data.ChurchId)
            .OrderBy(s => s.FirstName)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        
        return Result.Ok(students);
    }

    public async Task<Result<StudentDto>> GetById(int id)
    {
        var roles = _authUserService.GetUserRoles();
        if (roles.Contains(Roles.Admin) || roles.Contains(Roles.SuperAdmin))
        {
            var student = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id);
            
            return student == null
                ? Result.NotFound<StudentDto>("Student not found")
                : Result.Ok(student);
        }
        else
        {
            var user = await _identityQueries.GetById(_authUserService.GetUserId());
            if (user.Data == null)
                return Result.NotFound<StudentDto>("User not found");
            
            var student = await _context.Students
                .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(s => s.Id == id && s.ChurchId == user.Data.ChurchId);
            return student == null
                ? Result.NotFound<StudentDto>("Student not found")
                : Result.Ok(student);
        }
    }
}