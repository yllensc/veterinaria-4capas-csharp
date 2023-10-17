using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Repository;
using Domain.Interfaces;
using Persistence;

namespace Application.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly VeterinaryDbContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    private IUserRol _userole;
    private IAppointmentRepository _appointmentRepository;
    private ILaboratoryRepository _laboratoryRepository;
    private IMedicineRepository _medicineRepository;
    private IMedicineMovementRepository _medicineMovementRepository;
    private IOwnerRepository _ownerRepository;
    private IPetRepository _petRepository;
    private IProviderRepository _providerRepository;
    private IRaceRepository _raceRepository;
    private ISpecieRepository _specieRepository;
    private ITreatmentRepository _treatmentRepository;
    private ITypeMovementRepository _typeMovementRepository;
    private IVeterinarianRepository _veterinarianRepository;
    
    public UnitOfWork(VeterinaryDbContext context)
    {
        _context = context;
    }
    public IRolRepository Roles
    {
        get
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }
    public IUserRol UserRoles
    {
        get
        {
            if (_userole == null)
            {
                _userole = new UseroleRepository(_context);
            }
            return _userole;
        }
    }

    public IUserRepository Users
    {
        get
        {
            if (_users == null)
            {
                _users = new UserRepository(_context);
            }
            return _users;
        }
    }

    public IAppointmentRepository Appointments
    {
        get
        {
            if (_appointmentRepository == null)
            {
                _appointmentRepository = new AppointmentRepository(_context);
            }
            return _appointmentRepository;
        }
    }
    public ILaboratoryRepository Laboratories 
    {
        get
        {
            if (_laboratoryRepository == null)
            {
                _laboratoryRepository = new LaboratoryRepository(_context);
            }
            return _laboratoryRepository;
        }
    }

    public IMedicineRepository Medicines
    {
        get
        {
            if (_medicineRepository == null)
            {
                _medicineRepository = new MedicineRepository(_context);
            }
            return _medicineRepository;
        }
    }

    public IMedicineMovementRepository MedicineMovements
    {
        get
        {
            if (_medicineMovementRepository == null)
            {
                _medicineMovementRepository = new MedicineMovementRepository(_context);
            }
            return _medicineMovementRepository;
        }
    }

    public IOwnerRepository Owners 
    {
        get
        {
            if (_ownerRepository == null)
            {
                _ownerRepository = new OwnerRepository(_context);
            }
            return _ownerRepository;
        }
    }

    public IRaceRepository Races
    {
        get
        {
            if (_raceRepository == null)
            {
                _raceRepository = new RaceRepository(_context);
            }
            return _raceRepository;
        }
    }

    public ISpecieRepository Species
    {
        get
        {
            if (_specieRepository == null)
            {
                _specieRepository = new SpecieRepository(_context);
            }
            return _specieRepository;
        }
    }

    public ITreatmentRepository Treatments
    {
        get
        {
            if (_treatmentRepository == null)
            {
                _treatmentRepository = new TreatmentRepository(_context);
            }
            return _treatmentRepository;
        }
    }

    public ITypeMovementRepository TypeMovements
    {
        get
        {
            if (_typeMovementRepository == null)
            {
                _typeMovementRepository = new TypeMovementRepository(_context);
            }
            return _typeMovementRepository;
        }
    }

    public IVeterinarianRepository Veterinarians
    {
        get
        {
            if (_veterinarianRepository == null)
            {
                _veterinarianRepository = new VeterinarianRepository(_context);
            }
            return _veterinarianRepository;
        }
    }

    public IPetRepository Pets
    {
        get
        {
            if (_petRepository == null)
            {
                _petRepository = new PetRepository(_context);
            }
            return _petRepository;
        }
    }

    public IProviderRepository Providers
    {
        get
        {
            if (_providerRepository == null)
            {
                _providerRepository = new ProviderRepository(_context);
            }
            return _providerRepository;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
