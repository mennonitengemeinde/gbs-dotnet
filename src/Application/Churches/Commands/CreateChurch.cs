using FluentValidation;
using MediatR;

namespace Gbs.Application.Churches.Commands;

public record CreateChurchCommand(CreateChurchRequest Church) : IRequest<Result<ChurchResponse>>;

public class CreateChurchCommandValidator : AbstractValidator<CreateChurchCommand>
{
    private readonly IGbsDbContext _context;

    public CreateChurchCommandValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(c => c.Church).SetValidator(new CreateChurchRequestValidator());
        
        RuleFor(c => c.Church.Name)
            .MustAsync(BeUniqueName).WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(string name, CancellationToken cancellationToken)
    {
        return await _context.Churches
            .AllAsync(c => c.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}

public class CreateChurchCommandHandler : IRequestHandler<CreateChurchCommand, Result<ChurchResponse>>
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public CreateChurchCommandHandler(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<ChurchResponse>> Handle(CreateChurchCommand request, CancellationToken cancellationToken)
    {
        var newChurch = _mapper.Map<Church>(request);
        _context.Churches.Add(newChurch);
        await _context.SaveChangesAsync(cancellationToken);
        return Result.Ok(_mapper.Map<ChurchResponse>(newChurch));
    }
}