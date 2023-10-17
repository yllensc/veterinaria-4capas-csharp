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
    public class RaceController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public RaceController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RaceDto>>> Get()
        {
            var Race = await _unitOfwork.Races.GetAllAsync();
            return _mapper.Map<List<RaceDto>>(Race);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RaceDto>> Get(int id)
        {
            var Race = await _unitOfwork.Races.GetByIdAsync(id);
            if (Race == null)
            {
                return NotFound();
            }
            return this._mapper.Map<RaceDto>(Race);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<RaceDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Races.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listRace = _mapper.Map<List<RaceDto>>(records);
            return new Pager<RaceDto>(listRace, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Race>> Post(RaceDto RaceDto)
        {
            var Race = this._mapper.Map<Race>(RaceDto);
            this._unitOfwork.Races.Add(Race);
            await _unitOfwork.SaveAsync();
            if (Race == null)
            {
                return BadRequest();
            }
            RaceDto.Id = Race.Id;
            return CreatedAtAction(nameof(Post), new { id = RaceDto.Id }, RaceDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RaceDto>> Put(int id, [FromBody]RaceDto RaceDto)
        {
            if (RaceDto == null)
            {
                return NotFound();
            }
            var Race = this._mapper.Map<Race>(RaceDto);
            _unitOfwork.Races.Update(Race);
            await _unitOfwork.SaveAsync();
            return RaceDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Race = await _unitOfwork.Races.GetByIdAsync(id);
            if (Race == null)
            {
                return NotFound();
            }
            _unitOfwork.Races.Remove(Race);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}