using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
    public class AppointmentRepository: GenericRepository<Appointment>, IAppointmentRepository
{
    private readonly VeterinaryDbContext _context;

    public AppointmentRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }

    public override async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments
            .Include(p => p.Pet)
            .ToListAsync();
    }

    public override async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments
            .Include(p => p.Pet)
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}