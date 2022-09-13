﻿namespace gbs.Server.Services.GenerationService;

public class GenerationService : IGenerationService
{
    private readonly DataContext _context;

    public GenerationService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<Generation>>> GetAllGenerations()
    {
        var response = new ServiceResponse<List<Generation>>
        {
            Data = await _context.Generations.ToListAsync()
        };
        return response;
    }

    public async Task<ServiceResponse<Generation>> GetGenerationById(int id)
    {
        var user = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            return new ServiceResponse<Generation>
            {
                Success = false,
                Message = "Generation not found."
            };
        }

        return new ServiceResponse<Generation>
        {
            Data = user
        };
    }

    public async Task<ServiceResponse<Generation>> AddGeneration(CreateGenerationDto generation)
    {
        var newGeneration = new Generation
        {
            Name = generation.Name
        };
        await _context.Generations.AddAsync(newGeneration);
        await _context.SaveChangesAsync();
        return new ServiceResponse<Generation>
        {
            Data = newGeneration
        };
    }

    public async Task<ServiceResponse<Generation>> UpdateGeneration(int generationId, UpdateGenerationDto generation)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == generationId);
        if (dbGeneration == null)
        {
            return new ServiceResponse<Generation>
            {
                Success = false,
                Message = "Generation not found."
            };
        }

        dbGeneration.Name = generation.Name;
        await _context.SaveChangesAsync();
        return new ServiceResponse<Generation>
        {
            Data = dbGeneration
        };
    }

    public async Task<ServiceResponse<bool>> DeleteGeneration(int id)
    {
        var dbGeneration = await _context.Generations.FirstOrDefaultAsync(u => u.Id == id);
        if (dbGeneration == null)
        {
            return new ServiceResponse<bool>
            {
                Success = false,
                Message = "Generation not found."
            };
        }

        _context.Generations.Remove(dbGeneration);
        await _context.SaveChangesAsync();
        return new ServiceResponse<bool>
        {
            Data = true
        };
    }
}