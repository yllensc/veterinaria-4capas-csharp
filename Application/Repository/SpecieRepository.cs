using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class SpecieRepository: GenericRepository<Specie>, ISpecieRepository
{
    private readonly VeterinaryDbContext _context;

    public SpecieRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}