using Gbs.Application.Common.Interfaces.Services;
using Gbs.Application.Features.Identity.Interfaces;
using Gbs.Application.Features.Students.Interfaces;

namespace Gbs.Application.Features.Students;

public class StudentCommands : IStudentCommands
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;
    private readonly IIdentityQueries _identityQueries;
    private readonly IAuthenticatedUserService _authUserService;

    public StudentCommands(
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
    
    public async Task<Result<StudentResponse>> Add(CreateStudentRequest request)
    {
        var student = _mapper.Map<Student>(request);
        
        if (!_authUserService.UserIsAdmin())
        {
            var user = await _identityQueries.GetById(_authUserService.GetUserId());
            if (user.Data?.ChurchId == null)
                return Result.BadRequest<StudentResponse>("You are not assigned to a church");
            
            student.ChurchId = user.Data.ChurchId.Value;
        }
        
        _context.Students.Add(student);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<StudentResponse>(student));
    }

    public async Task<Result<StudentResponse>> Update(int id, CreateStudentRequest request)
    {
        var student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
        if (student == null)
            return Result.NotFound<StudentResponse>("Student not found");

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Address = request.Address;
        student.City = request.City;
        student.Province = request.Province;
        student.PostalCode = request.PostalCode;
        student.Country = request.Country;
        if (request.DateOfBirth != null) student.DateOfBirth = request.DateOfBirth.Value;
        student.MaritalStatus = request.MaritalStatus;
        student.Email = request.Email;
        student.Phone = request.Phone;
        
        if (_authUserService.UserIsAdmin())
        {
            student.ChurchId = request.ChurchId;
        }

        _context.Students.Update(student);
        await _context.SaveChangesAsync();
        return Result.Ok(_mapper.Map<StudentResponse>(student));
    }

    public Task<Result<bool>> Delete(int id)
    {
        throw new NotImplementedException();
    }
}