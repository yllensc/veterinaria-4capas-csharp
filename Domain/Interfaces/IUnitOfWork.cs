using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUserRepository Users { get; }
    IUserRol UserRoles {get; }
    IAppointmentRepository Appointments {get; }
    ILaboratoryRepository Laboratories {get; }
    IMedicineRepository Medicines {get; }
    IMedicineMovementRepository MedicineMovements {get; }
    IOwnerRepository Owners {get; }
    IPetRepository Pets {get; }
    IProviderRepository Providers {get; }
    IRaceRepository Races {get; }
    ISpecieRepository Species {get; }
    ITreatmentRepository Treatments {get; }
    ITypeMovementRepository TypeMovements {get; }
    IVeterinarianRepository Veterinarians {get; }
    Task<int> SaveAsync();

}
