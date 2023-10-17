using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class AppointmentRepository: GenericRepository<Appointment>, IAppointmentRepository
{
    private readonly VeterinaryDbContext _context;

    public AppointmentRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}