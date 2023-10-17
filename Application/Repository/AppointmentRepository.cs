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
        .FirstOrDefaultAsync(p =>  p.Id == id);
    }
}