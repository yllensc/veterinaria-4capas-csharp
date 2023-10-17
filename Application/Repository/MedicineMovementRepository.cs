using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class MedicineMovementRepository: GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    private readonly VeterinaryDbContext _context;

    public MedicineMovementRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}