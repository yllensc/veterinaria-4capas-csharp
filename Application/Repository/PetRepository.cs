using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class PetRepository : GenericRepository<Pet>, IPetRepository
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
    public override async Task<IEnumerable<Pet>> GetAllAsync()
    {
        return await _context.Pets
            .Include(p => p.Owner)
            .Include(p => p.Specie)
            .Include(p => p.Race)
            .ToListAsync();
    }
    public override async Task<(int totalRecords, IEnumerable<Pet> records)> GetAllAsync(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Owner)
            .Include(p => p.Specie)
            .Include(p => p.Race)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
    public async Task<IEnumerable<Pet>> GetPetsWithXRace(string race)
    {
        var pets = await _context.Pets
                    .Include(p => p.Race)
                    .Include(p => p.Owner)
                    .Where(p => p.Race.Name.ToLower().Equals(race.ToLower()))
                    .ToListAsync();
        return pets;
    }
    public async Task<IEnumerable<object>> GetPetsByRace()
{
    var pets = await _context.Pets
        .Include(p => p.Race)
        .GroupBy(g => g.Race.Name)
        .Select(u => new
        {
            NameRace = u.Key,
            TotalPets = u.Count()
        })
        .ToListAsync();

    return pets;
}

    public async Task<(int totalRecords, IEnumerable<Pet> records)> GetPetsWithXRace(string race, int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;

        if (!String.IsNullOrEmpty(race))
        {
            query = query.Where(p => p.Race.Name == race);
        }

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Name == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Race)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }
    public async Task<(int totalRecords, IEnumerable<object> records)> GetPetsByRace(int pageIndex, int pageSize, string search)
    {
        var query = _context.Pets as IQueryable<Pet>;

        if (!String.IsNullOrEmpty(search))
        {
            query = query.Where(p => p.Race.Name.Contains(search));
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var pets = await _context.Pets
        .Include(p => p.Race)
        .GroupBy(g => g.Race.Name)
        .Select(u => new
        {
            NameRace = u.Key,
            TotalPets = u.Count()
        })
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, pets);
    }

}