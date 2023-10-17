using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly VeterinaryDbContext _context;

    public RolRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}
