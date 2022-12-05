namespace Gbs.Application.Churches.Commands;

public record UpdateChurchCommand(int id, CreateChurchRequest Request) : IRequest<Result<ChurchDto>>;

public class UpdateChurchCommandValidator : AbstractValidator<UpdateChurchCommand>
{
    private readonly IGbsDbContext _context;

    public UpdateChurchCommandValidator(IGbsDbContext context)
    {
        _context = context;

        RuleFor(c => c.Request).SetValidator(new CreateChurchRequestValidator());

        RuleFor(c => c.Request.Name)
            .MustAsync((c, name, cancellation) => BeUniqueName(c.id, name, cancellation))
            .WithMessage("The specified name already exists.");
    }

    private async Task<bool> BeUniqueName(int id, string name, CancellationToken cancellationToken)
    {
        return await _context.Churches
            .AllAsync(c => c.Name.ToLower().Equals(name.ToLower()) && c.Id != id, cancellationToken);
    }
}

public class UpdateChurchCommandHandler : IRequestHandler<UpdateChurchCommand, Result<ChurchDto>>
{
    private readonly IGbsDbContext _context;
    private readonly IMapper _mapper;

    public UpdateChurchCommandHandler(IGbsDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result<ChurchDto>> Handle(UpdateChurchCommand request, CancellationToken cancellationToken)
    {
        var dbChurch = await _context.Churches.FirstOrDefaultAsync(c => c.Id == request.id, cancellationToken);
        if (dbChurch == null)
        {
            return Result.NotFound<ChurchDto>("Church not found");
        }

        dbChurch.Name = request.Request.Name;
        dbChurch.Address = request.Request.Address;
        dbChurch.City = request.Request.City;
        dbChurch.State = request.Request.State;
        dbChurch.PostalCode = request.Request.PostalCode;
        dbChurch.Country = request.Request.Country;
        _context.Churches.Update(dbChurch);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok(_mapper.Map<ChurchDto>(dbChurch));
    }
}