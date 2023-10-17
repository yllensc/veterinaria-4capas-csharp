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
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public PetController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PetDto>>> Get()
        {
            var pet = await unitofwork.Pets.GetAllAsync();
            return mapper.Map<List<PetDto>>(pet);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Get(int id)
        {
            var pet = await unitofwork.Pets.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return this.mapper.Map<PetDto>(pet);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<PetDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await unitofwork.Pets.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listPet = mapper.Map<List<PetDto>>(records);
            return new Pager<PetDto>(listPet, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pet>> Post(PetDto petDto)
        {
            var pet = this.mapper.Map<Pet>(petDto);
            this.unitofwork.Pets.Add(pet);
            await unitofwork.SaveAsync();
            if (pet == null)
            {
                return BadRequest();
            }
            petDto.Id = pet.Id;
            return CreatedAtAction(nameof(Post), new { id = petDto.Id }, petDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PetDto>> Put(int id, [FromBody]PetDto petDto)
        {
            if (petDto == null)
            {
                return NotFound();
            }
            var pet = this.mapper.Map<Pet>(petDto);
            unitofwork.Pets.Update(pet);
            await unitofwork.SaveAsync();
            return petDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var pet = await unitofwork.Pets.GetByIdAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            unitofwork.Pets.Remove(pet);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    }
}