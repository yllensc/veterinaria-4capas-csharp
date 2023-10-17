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
    public class OwnerController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public OwnerController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
        {
            var Owner = await _unitOfwork.Owners.GetAllAsync();
            return _mapper.Map<List<OwnerDto>>(Owner);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> Get(int id)
        {
            var Owner = await _unitOfwork.Owners.GetByIdAsync(id);
            if (Owner == null)
            {
                return NotFound();
            }
            return this._mapper.Map<OwnerDto>(Owner);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<OwnerDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Owners.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listOwner = _mapper.Map<List<OwnerDto>>(records);
            return new Pager<OwnerDto>(listOwner, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Owner>> Post(OwnerDto OwnerDto)
        {
            var Owner = this._mapper.Map<Owner>(OwnerDto);
            this._unitOfwork.Owners.Add(Owner);
            await _unitOfwork.SaveAsync();
            if (Owner == null)
            {
                return BadRequest();
            }
            OwnerDto.Id = Owner.Id;
            return CreatedAtAction(nameof(Post), new { id = OwnerDto.Id }, OwnerDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OwnerDto>> Put(int id, [FromBody]OwnerDto OwnerDto)
        {
            if (OwnerDto == null)
            {
                return NotFound();
            }
            var Owner = this._mapper.Map<Owner>(OwnerDto);
            _unitOfwork.Owners.Update(Owner);
            await _unitOfwork.SaveAsync();
            return OwnerDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Owner = await _unitOfwork.Owners.GetByIdAsync(id);
            if (Owner == null)
            {
                return NotFound();
            }
            _unitOfwork.Owners.Remove(Owner);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}