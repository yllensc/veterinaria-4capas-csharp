using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class TreatmentRepository: GenericRepository<Treatment>, ITreatmentRepository
{
    private readonly VeterinaryDbContext _context;

    public TreatmentRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}