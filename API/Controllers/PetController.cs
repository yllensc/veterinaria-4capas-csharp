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
    public class PetController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public PetController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PetDto>>> Get()
        {
            var Pet = await _unitOfwork.Pets.GetAllAsync();
            return _mapper.Map<List<PetDto>>(Pet);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Get(int id)
        {
            var Pet = await _unitOfwork.Pets.GetByIdAsync(id);
            if (Pet == null)
            {
                return NotFound();
            }
            return this._mapper.Map<PetDto>(Pet);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PetDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Pets.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listPet = _mapper.Map<List<PetDto>>(records);
            return new Pager<PetDto>(listPet, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(PetDto model)
        {
        var Pet = _mapper.Map<Pet>(model);
        var result = await _unitOfwork.Pets.RegisterAsync(Pet);
        return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Put(int id, [FromBody]PetDto PetDto)
        {
            if (PetDto == null)
            {
                return NotFound();
            }
            var Pet = this._mapper.Map<Pet>(PetDto);
            _unitOfwork.Pets.Update(Pet);
            await _unitOfwork.SaveAsync();
            return PetDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Pet = await _unitOfwork.Pets.GetByIdAsync(id);
            if (Pet == null)
            {
                return NotFound();
            }
            _unitOfwork.Pets.Remove(Pet);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}