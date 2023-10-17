using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class VeterinarianRepository: GenericRepository<Veterinarian>, IVeterinarianRepository
{
    private readonly VeterinaryDbContext _context;

    public VeterinarianRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}