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
    [Authorize (Roles = "Administrator")]
    public class VeterinarianController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public VeterinarianController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VeterinarianDto>>> Get()
        {
            var Veterinarian = await _unitOfwork.Veterinarians.GetAllAsync();
            return _mapper.Map<List<VeterinarianDto>>(Veterinarian);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeterinarianDto>> Get(int id)
        {
            var Veterinarian = await _unitOfwork.Veterinarians.GetByIdAsync(id);
            if (Veterinarian == null)
            {
                return NotFound();
            }
            return this._mapper.Map<VeterinarianDto>(Veterinarian);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VeterinarianDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Veterinarians.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listVeterinarian = _mapper.Map<List<VeterinarianDto>>(records);
            return new Pager<VeterinarianDto>(listVeterinarian, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeterinarianDto>> Put(int id, [FromBody] VeterinarianDto VeterinarianDto)
        {
            if (VeterinarianDto == null)
            {
                return NotFound();
            }
            var Veterinarian = this._mapper.Map<Veterinarian>(VeterinarianDto);
            _unitOfwork.Veterinarians.Update(Veterinarian);
            await _unitOfwork.SaveAsync();
            return VeterinarianDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Veterinarian = await _unitOfwork.Veterinarians.GetByIdAsync(id);
            if (Veterinarian == null)
            {
                return NotFound();
            }
            _unitOfwork.Veterinarians.Remove(Veterinarian);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
        //Endpoints
        [HttpGet("cardiovascularSurgeonVeterinarian")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VeterinarianDto>>> Get1()
        {
            var veterinarians = await _unitOfwork.Veterinarians.GetCardiovascularSurgeonAsync();
            if (veterinarians == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<VeterinarianDto>>(veterinarians);
        }

        [HttpGet("cardiovascularSurgeonVeterinarian")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VeterinarianDto>>> GetPaginationEnd1([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Veterinarians.GetCardiovascularSurgeonAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listVeterinarian = _mapper.Map<List<VeterinarianDto>>(records);
            return new Pager<VeterinarianDto>(listVeterinarian, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }
    }
}