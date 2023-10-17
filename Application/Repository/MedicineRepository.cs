using Domain.Entities;
using Domain.Interfaces;
using Persistence;

namespace Application.Repository;
    public class MedicineRepository: GenericRepository<Medicine>, IMedicineRepository
{
    private readonly VeterinaryDbContext _context;

    public MedicineRepository(VeterinaryDbContext context) : base(context)
    {
       _context = context;
    }
}