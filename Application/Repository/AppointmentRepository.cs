using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    private readonly VeterinaryDbContext _context;
    public AppointmentRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<string> RegisterAsync(Appointment model)
    {
        // Verificar si la mascota (Pet) existe
        var existingPet = _context.Pets
            .Where(p => p.Id == model.IdPet)
            .FirstOrDefault();

        if (existingPet == null)
        {
            return "La mascota no existe en nuestro sistema";
        }

        // Verificar si el veterinario existe
        var existingVeterinarian = _context.Veterinarians
            .Where(v => v.Id == model.IdVeterinarian)
            .FirstOrDefault();

        if (existingVeterinarian == null)
        {
            return "El veterinario no existe en nuestro sistema";
        }

        // Crear la cita
        var appointment = new Appointment
        {
            DateAppointment = model.DateAppointment,
            Hour = model.Hour,
            Cause = model.Cause,
            Pet = existingPet,
            Veterinarian = existingVeterinarian,
        };

        try
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return $"Cita registrada con éxito";
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return $"Error al registrar la cita: {message}";
        }
    }


    public override async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(p => p.Pet)
            .ToListAsync();
    }
    public override async Task<(int totalRecords, IEnumerable<Appointment> records)> GetAllAsync(int pageIndex, int pageSize, int search)
    {
        var query = _context.Appointments as IQueryable<Appointment>;

        if (search != 0)
        {
            query = query.Where(p => p.IdVeterinarian == search);
        }

        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Pet)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (totalRecords, records);
    }

    public override async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(p => p.Pet)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
}