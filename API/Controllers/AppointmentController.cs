using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize]
    public class AppointmentController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public AppointmentController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get()
        {
            var Appointment = await _unitOfwork.Appointments.GetAllAsync();
            return _mapper.Map<List<AppointmentDto>>(Appointment);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentDto>> Get(int id)
        {
            var Appointment = await _unitOfwork.Appointments.GetByIdAsync(id);
            if (Appointment == null)
            {
                return NotFound();
            }
            return this._mapper.Map<AppointmentDto>(Appointment);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<AppointmentDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Appointments.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listAppointment = _mapper.Map<List<AppointmentDto>>(records);
            return new Pager<AppointmentDto>(listAppointment, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(AppointmentDto model)
        {
            var appointment = _mapper.Map<Appointment>(model);
            var result = await _unitOfwork.Appointments.RegisterAsync(appointment);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentDto>> Put(int id, [FromBody] AppointmentDto AppointmentDto)
        {
            if (AppointmentDto == null)
            {
                return NotFound();
            }
            var Appointment = this._mapper.Map<Appointment>(AppointmentDto);
            _unitOfwork.Appointments.Update(Appointment);
            await _unitOfwork.SaveAsync();
            return AppointmentDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Appointment = await _unitOfwork.Appointments.GetByIdAsync(id);
            if (Appointment == null)
            {
                return NotFound();
            }
            _unitOfwork.Appointments.Remove(Appointment);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
        //Endpoints
        [HttpGet("petsOn{year}On{quarter}for{cause}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AppointmentWithPets>>> Get6(string cause, int quarter, int year)
        {
            var appointments = await _unitOfwork.Appointments.GetPetsByAppointmentEspecific(cause, quarter, year);
            if (appointments == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<AppointmentWithPets>>(appointments);
        }
        [HttpGet("petsOn{year}On{quarter}for{cause}")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<AppointmentWithPets>>> GetPaginationEnd6([FromQuery] Params Params, string cause, int quarter, int year)
        {
            var (totalRecords, records) = await _unitOfwork.Appointments.GetPetsByAppointmentEspecific(cause, quarter, year, Params.PageIndex, Params.PageSize, Params.Search);
            var listAppointment = _mapper.Map<List<AppointmentWithPets>>(records);
            return new Pager<AppointmentWithPets>(listAppointment, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);

        }
        [HttpGet("petsCaredByVeterinarian{IdVeterinarian}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AppointmentWithPets>>> Get9(int IdVeterinarian)
        {
            var appointments = await _unitOfwork.Appointments.GetPetsOnAppointmentWithVeterinarianX(IdVeterinarian);
            if (appointments == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<AppointmentWithPets>>(appointments);
        }
        [HttpGet("petsCaredByVeterinarian{IdVeterinarian}")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<AppointmentWithPets>>> GetPaginationEnd9([FromQuery] Params Params, int IdVeterinarian)
        {
            var (totalRecords, records) = await _unitOfwork.Appointments.GetPetsOnAppointmentWithVeterinarianX(IdVeterinarian, Params.PageIndex, Params.PageSize, Params.Search);
            var listAppointment = _mapper.Map<List<AppointmentWithPets>>(records);
            return new Pager<AppointmentWithPets>(listAppointment, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);

        }
    }
}