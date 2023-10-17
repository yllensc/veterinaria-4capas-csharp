using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class PetRepository: GenericRepository<Pet>, IPetRepository
{
    private readonly VeterinaryDbContext _context;

    public PetRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}