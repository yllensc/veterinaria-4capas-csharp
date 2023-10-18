using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<string> RegisterAsync(Appointment model);
        Task<IEnumerable<Appointment>> GetPetsByAppointmentEspecific(string cause, int quarter, int year);
        Task<IEnumerable<Appointment>> GetPetsOnAppointmentWithVeterinarianX(int IdVeterinarian);

    }
}