using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class TypeMovementRepository: GenericRepository<TypeMovement>, ITypeMovementRepository
{
    private readonly VeterinaryDbContext _context;

    public TypeMovementRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}