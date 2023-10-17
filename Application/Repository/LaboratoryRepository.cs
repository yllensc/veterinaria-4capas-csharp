using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class LaboratoryRepository: GenericRepository<Laboratory>, ILaboratoryRepository
{
    private readonly VeterinaryDbContext _context;

    public LaboratoryRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}