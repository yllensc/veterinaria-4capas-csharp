using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class OwnerRepository: GenericRepository<Owner>, IOwnerRepository
{
    private readonly VeterinaryDbContext _context;

    public OwnerRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}