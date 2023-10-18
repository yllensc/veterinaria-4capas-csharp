using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class PetRepository: GenericRepository<Pet>, IPetRepository
{
    private readonly VeterinaryDbContext _context;

    public PetRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }

    public async Task<string> RegisterAsync(Pet model)
    {
        var existingOwner = _context.Owners
            .Where(p => p.Id == model.IdOwner)
            .FirstOrDefault();

        if (existingOwner == null)
        {
            return "El propietario no existe en nuestro sistema";
        }

        var existingSpecie = _context.Species
            .Where(v => v.Id == model.IdSpecie)
            .FirstOrDefault();

        if (existingSpecie == null)
        {
            return "La especie no existe en nuestro sistema";
        }
        var existingRace = _context.Races
            .Where(v => v.Id == model.IdRace)
            .FirstOrDefault();

        if (existingRace == null)
        {
            return "La raza no existe en nuestro sistema";
        }

        // Crear la mascota
        var pet = new Pet
        {
            Name = model.Name,
            BirthDate = model.BirthDate,
            Race = existingRace,
            Owner = existingOwner,
            Specie = existingSpecie
        };

        try
        {
            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            return $"Cita registrada con éxito";
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return $"Error al registrar la cita: {message}";
        }
    }
    
}