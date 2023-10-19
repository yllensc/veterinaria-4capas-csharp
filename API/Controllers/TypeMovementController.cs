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
    public class TypeMovementController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public TypeMovementController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TypeMovementDto>>> Get()
        {
            var TypeMovement = await _unitOfwork.TypeMovements.GetAllAsync();
            return _mapper.Map<List<TypeMovementDto>>(TypeMovement);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeMovementDto>> Get(int id)
        {
            var TypeMovement = await _unitOfwork.TypeMovements.GetByIdAsync(id);
            if (TypeMovement == null)
            {
                return NotFound();
            }
            return this._mapper.Map<TypeMovementDto>(TypeMovement);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<TypeMovementDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.TypeMovements.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listTypeMovement = _mapper.Map<List<TypeMovementDto>>(records);
            return new Pager<TypeMovementDto>(listTypeMovement, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TypeMovement>> Post(TypeMovementDto TypeMovementDto)
        {
            var TypeMovement = this._mapper.Map<TypeMovement>(TypeMovementDto);
            this._unitOfwork.TypeMovements.Add(TypeMovement);
            await _unitOfwork.SaveAsync();
            if (TypeMovement == null)
            {
                return BadRequest();
            }
            TypeMovementDto.Id = TypeMovement.Id;
            return CreatedAtAction(nameof(Post), new { id = TypeMovementDto.Id }, TypeMovementDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TypeMovementDto>> Put(int id, [FromBody] TypeMovementDto TypeMovementDto)
        {
            if (TypeMovementDto == null)
            {
                return NotFound();
            }
            var TypeMovement = this._mapper.Map<TypeMovement>(TypeMovementDto);
            _unitOfwork.TypeMovements.Update(TypeMovement);
            await _unitOfwork.SaveAsync();
            return TypeMovementDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var TypeMovement = await _unitOfwork.TypeMovements.GetByIdAsync(id);
            if (TypeMovement == null)
            {
                return NotFound();
            }
            _unitOfwork.TypeMovements.Remove(TypeMovement);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}