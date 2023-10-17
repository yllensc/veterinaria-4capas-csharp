using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class RaceRepository: GenericRepository<Race>, IRaceRepository
{
    private readonly VeterinaryDbContext _context;

    public RaceRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}