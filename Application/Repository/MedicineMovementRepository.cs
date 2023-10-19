using System.Globalization;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Repository;
public class MedicineMovementRepository : GenericRepository<MedicineMovement>, IMedicineMovementRepository
{
    private readonly VeterinaryDbContext _context;

    public MedicineMovementRepository(VeterinaryDbContext context) : base(context)
    {
        _context = context;
    }
    public override async Task<IEnumerable<MedicineMovement>> GetAllAsync()
    {
        return await _context.MedicineMovements
            .Include(p => p.Medicine)
            .Include(p => p.TypeMovement)
            .ToListAsync();
    }

    public override async Task<MedicineMovement> GetByIdAsync(int id)
    {
        return await _context.MedicineMovements
        .Include(p => p.Medicine)
        .Include(p => p.TypeMovement)
        .FirstOrDefaultAsync(p => p.Id == id);
    }
    public override async Task<(int totalRecords, IEnumerable<MedicineMovement> records)> GetAllAsync(int pageIndex, int pageSize, int search)
    {
        var query = _context.MedicineMovements as IQueryable<MedicineMovement>;
        if (search != 0)
        {
            query = query.Where(p => p.Id == search);
        }
        query = query.OrderBy(p => p.Id);
        var totalRecords = await query.CountAsync();
        var records = await query
            .Include(p => p.Medicine)
            .Include(p => p.TypeMovement)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRecords, records);
    }
    public async Task<string> RegisterAsync(MedicineMovement model)
    {
        string strDateMovement = model.DateMovement.ToString("yyyy-MM-ddTHH:mm:ss.ffffffZ");
        try
        {
            if (DateTime.TryParseExact(strDateMovement, "yyyy-MM-ddTHH:mm:ss.ffffffZ", null, DateTimeStyles.None, out DateTime parseDate))
            {
                var medicine = _context.Medicines
                                .Where(m => m.Id == model.IdMedicine)
                                .FirstOrDefault();
                if (medicine == null)
                {
                    return "medicina no encontrada en el sistema";
                }
                else
                {
                    int quantityStock = medicine.QuantityDisp;
                    if (model.IdTypeMovement == 1)
                    {
                        medicine.QuantityDisp += model.Quantity;
                        _context.Medicines.Update(medicine);
                        await _context.SaveChangesAsync();
                    }
                    else if (model.IdTypeMovement == 2 && model.Quantity <= quantityStock)
                    {
                        medicine.QuantityDisp -= model.Quantity;
                        _context.Medicines.Update(medicine);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return "sorry, no pudimos realizar la acción, verifique el tipo de movimiento y la cantidad de medicina disponible";
                    }


                    var movement = new MedicineMovement
                    {
                        Quantity = model.Quantity,
                        DateMovement = model.DateMovement,
                        PriceUnit = model.PriceUnit,
                        IdMedicine = model.IdMedicine,
                        IdTypeMovement = model.IdTypeMovement
                    };
                    _context.MedicineMovements.Add(movement);
                    await _context.SaveChangesAsync();
                    return $"Movimiento registrado con éxito";
                }
            }
            else
            {
                return "fecha inválida, ojito con el formato yyyy-mm-dd";
            }
        }
        catch (Exception ex)
        {
            var message = ex.Message;
            return $"Error al registrar la cita: {message}";
        }
    }

    public async Task<IEnumerable<object>> GetListMovements()
    {
        var movements = _context.MedicineMovements
        .Select(movement => new
        {
            movement.DateMovement,
            movement.IdMedicine,
            movement.IdTypeMovement,
            TypeName = _context.TypeMovements
                .Where(typeMovement => typeMovement.Id == movement.IdTypeMovement)
                .Select(typeMovement => typeMovement.Name)
                .FirstOrDefault(),
            movement.Quantity,
            movement.PriceUnit,
            TotalMovement = movement.Quantity * movement.PriceUnit
        })
        .ToListAsync();
        return await movements;
    }

    public async Task<(int totalRecords, IEnumerable<object> records)> GetListMovements(int pageIndex, int pageSize, string search)
    {
        var query = _context.MedicineMovements as IQueryable<MedicineMovement>;

        if (!string.IsNullOrEmpty(search) && DateTime.TryParse(search, out var searchDate))
        {
            searchDate = searchDate.Date;
            query = query.Where(m => m.DateMovement.Date == searchDate);
        }

        query = query.OrderBy(m => m.Id);

        var totalRecords = await query.CountAsync();

        var movements = await query
            .Select(movement => new
            {
                movement.DateMovement,
                movement.IdMedicine,
                movement.IdTypeMovement,
                movement.Quantity,
                movement.PriceUnit,
                TotalMovement = movement.Quantity * movement.PriceUnit,
                TypeName = _context.TypeMovements
                    .Where(typeMovement => typeMovement.Id == movement.IdTypeMovement)
                    .Select(typeMovement => typeMovement.Name)
                    .FirstOrDefault()
            })
            .ToListAsync();

        return (totalRecords, movements);
    }



}