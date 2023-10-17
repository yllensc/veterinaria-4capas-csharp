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
    public class SpecieController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public SpecieController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpecieDto>>> Get()
        {
            var Specie = await _unitOfwork.Species.GetAllAsync();
            return _mapper.Map<List<SpecieDto>>(Specie);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecieDto>> Get(int id)
        {
            var Specie = await _unitOfwork.Species.GetByIdAsync(id);
            if (Specie == null)
            {
                return NotFound();
            }
            return this._mapper.Map<SpecieDto>(Specie);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<SpecieDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Species.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listSpecie = _mapper.Map<List<SpecieDto>>(records);
            return new Pager<SpecieDto>(listSpecie, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Specie>> Post(SpecieDto SpecieDto)
        {
            var Specie = this._mapper.Map<Specie>(SpecieDto);
            this._unitOfwork.Species.Add(Specie);
            await _unitOfwork.SaveAsync();
            if (Specie == null)
            {
                return BadRequest();
            }
            SpecieDto.Id = Specie.Id;
            return CreatedAtAction(nameof(Post), new { id = SpecieDto.Id }, SpecieDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SpecieDto>> Put(int id, [FromBody]SpecieDto SpecieDto)
        {
            if (SpecieDto == null)
            {
                return NotFound();
            }
            var Specie = this._mapper.Map<Specie>(SpecieDto);
            _unitOfwork.Species.Update(Specie);
            await _unitOfwork.SaveAsync();
            return SpecieDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Specie = await _unitOfwork.Species.GetByIdAsync(id);
            if (Specie == null)
            {
                return NotFound();
            }
            _unitOfwork.Species.Remove(Specie);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}