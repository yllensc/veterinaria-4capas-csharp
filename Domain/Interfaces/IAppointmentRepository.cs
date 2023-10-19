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
        Task<(int totalRecords, IEnumerable<Appointment> records)> GetPetsByAppointmentEspecific(string cause, int quarter, int year, int pageIndex, int pageSize, string search);
        Task<IEnumerable<Appointment>> GetPetsOnAppointmentWithVeterinarianX(int IdVeterinarian);
        Task<(int totalRecords, IEnumerable<Appointment> records)> GetPetsOnAppointmentWithVeterinarianX(int IdVeterinarian, int pageIndex, int pageSize, string search);

    }
}