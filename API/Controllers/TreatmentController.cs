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
    public class TreatmentController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public TreatmentController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TreatmentDto>>> Get()
        {
            var Treatment = await _unitOfwork.Treatments.GetAllAsync();
            return _mapper.Map<List<TreatmentDto>>(Treatment);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TreatmentDto>> Get(int id)
        {
            var Treatment = await _unitOfwork.Treatments.GetByIdAsync(id);
            if (Treatment == null)
            {
                return NotFound();
            }
            return this._mapper.Map<TreatmentDto>(Treatment);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TreatmentDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Treatments.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listTreatment = _mapper.Map<List<TreatmentDto>>(records);
            return new Pager<TreatmentDto>(listTreatment, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Treatment>> Post(TreatmentDto TreatmentDto)
        {
            var Treatment = this._mapper.Map<Treatment>(TreatmentDto);
            this._unitOfwork.Treatments.Add(Treatment);
            await _unitOfwork.SaveAsync();
            if (Treatment == null)
            {
                return BadRequest();
            }
            TreatmentDto.Id = Treatment.Id;
            return CreatedAtAction(nameof(Post), new { id = TreatmentDto.Id }, TreatmentDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TreatmentDto>> Put(int id, [FromBody] TreatmentDto TreatmentDto)
        {
            if (TreatmentDto == null)
            {
                return NotFound();
            }
            var Treatment = this._mapper.Map<Treatment>(TreatmentDto);
            _unitOfwork.Treatments.Update(Treatment);
            await _unitOfwork.SaveAsync();
            return TreatmentDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Treatment = await _unitOfwork.Treatments.GetByIdAsync(id);
            if (Treatment == null)
            {
                return NotFound();
            }
            _unitOfwork.Treatments.Remove(Treatment);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}