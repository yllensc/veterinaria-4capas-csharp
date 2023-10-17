using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize]
public class AppointmentController : ApiBaseController
{
    private readonly IUnitOfWork unitOfWork;
    private readonly  IMapper mapper;

    public AppointmentController( IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }
    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> Get()
    {
        var appointment = await unitOfWork.Appointments.GetAllAsync();
        return mapper.Map<List<AppointmentDto>>(appointment);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Get(int id)
    {
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id);
        if (appointment == null){
            return NotFound();
        }
        return this.mapper.Map<AppointmentDto>(appointment);
    }
    [HttpGet]
    [MapToApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<AppointmentDto>>> GetPagination([FromQuery] Params Params)
    {
        var (totalRecords, records) = await unitOfWork.Appointments.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
        var listAppointment = mapper.Map<List<AppointmentDto>>(records);
        return new Pager<AppointmentDto>(listAppointment, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
    }
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterAsync(AppointmentDto model)
    {
        var appointment = mapper.Map<Appointment>(model);
        var result = await unitOfWork.Appointments.RegisterAsync(appointment);
        return Ok(result);
    }
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<AppointmentDto>> Put(int id, [FromBody]AppointmentDto appointmentDto){
        if(appointmentDto == null)
        {
            return NotFound();
        }
        var appointment = this.mapper.Map<Appointment>(appointmentDto);
        unitOfWork.Appointments.Update(appointment);
        await unitOfWork.SaveAsync();
        return appointmentDto;
    }
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var appointment = await unitOfWork.Appointments.GetByIdAsync(id);
        if(appointment == null)
        {
            return NotFound();
        }
        unitOfWork.Appointments.Remove(appointment);
        await unitOfWork.SaveAsync();
        return NoContent();
    }
}