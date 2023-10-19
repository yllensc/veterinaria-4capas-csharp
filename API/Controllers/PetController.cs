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
        public async Task<ActionResult<PetGetDto>> Get(int id)
        {
            var Pet = await _unitOfwork.Pets.GetByIdAsync(id);
            if (Pet == null)
            {
                return NotFound();
            }
            return this._mapper.Map<PetGetDto>(Pet);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PetGetDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Pets.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listPet = _mapper.Map<List<PetGetDto>>(records);
            return new Pager<PetGetDto>(listPet, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
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
        public async Task<ActionResult<PetDto>> Put(int id, [FromBody] PetDto PetDto)
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

        //Endpoints
        [HttpGet("petsWithThis{race}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PetsBasicWithOwnerDto>>> Get11(string race)
        {
            var pets = await _unitOfwork.Pets.GetPetsWithXRace(race);
            if (pets == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<PetsBasicWithOwnerDto>>(pets);
        }
        [HttpGet("petsWithThis{race}")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PetsBasicWithOwnerDto>>> GetPaginationEnd11([FromQuery] Params Params, string race)
        {
            var (totalRecords, records) = await _unitOfwork.Pets.GetPetsWithXRace(race, Params.PageIndex, Params.PageSize, Params.Search);
            var listPet = _mapper.Map<List<PetsBasicWithOwnerDto>>(records);
            return new Pager<PetsBasicWithOwnerDto>(listPet, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpGet("countPetsByRace")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get12()
        {
            var pets = await _unitOfwork.Pets.GetPetsByRace();
            if (pets == null)
            {
                return NotFound();
            }
            return Ok(pets);
        }
        [HttpGet("countPetsByRace")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<object>>> GetPaginationEnd12([FromQuery] Params Params, string race)
        {
            var (totalRecords, records) = await _unitOfwork.Pets.GetPetsByRace(Params.PageIndex, Params.PageSize, Params.Search);
            var listPet = _mapper.Map<List<object>>(records);
            return new Pager<object>(listPet, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }
    }
}