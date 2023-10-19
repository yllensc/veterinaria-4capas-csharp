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
    public class MedicineMovementController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public MedicineMovementController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicineMovementDto>>> Get()
        {
            var MedicineMovement = await _unitOfwork.MedicineMovements.GetAllAsync();
            return _mapper.Map<List<MedicineMovementDto>>(MedicineMovement);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicineMovementDto>> Get(int id)
        {
            var MedicineMovement = await _unitOfwork.MedicineMovements.GetByIdAsync(id);
            if (MedicineMovement == null)
            {
                return NotFound();
            }
            return this._mapper.Map<MedicineMovementDto>(MedicineMovement);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicineMovementDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.MedicineMovements.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listMedicineMovement = _mapper.Map<List<MedicineMovementDto>>(records);
            return new Pager<MedicineMovementDto>(listMedicineMovement, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RegisterAsync(MedicineMovementDto model)
        {
            var medicineMovement = _mapper.Map<MedicineMovement>(model);
            var result = await _unitOfwork.MedicineMovements.RegisterAsync(medicineMovement);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicineMovementDto>> Put(int id, [FromBody] MedicineMovementDto MedicineMovementDto)
        {
            if (MedicineMovementDto == null)
            {
                return NotFound();
            }
            var MedicineMovement = this._mapper.Map<MedicineMovement>(MedicineMovementDto);
            _unitOfwork.MedicineMovements.Update(MedicineMovement);
            await _unitOfwork.SaveAsync();
            return MedicineMovementDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var MedicineMovement = await _unitOfwork.MedicineMovements.GetByIdAsync(id);
            if (MedicineMovement == null)
            {
                return NotFound();
            }
            _unitOfwork.MedicineMovements.Remove(MedicineMovement);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
        //Endpoints
        [HttpGet("listMovementsWithTotal")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Get8()
        {
            var movements = await _unitOfwork.MedicineMovements.GetListMovements();
            if (movements == null)
            {
                return NotFound();
            }
            return Ok(movements);
        }
        [HttpGet("listMovementsWithTotal")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<object>>> GetPaginationEnd8([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.MedicineMovements.GetListMovements(Params.PageIndex, Params.PageSize, Params.Search);
            var listMedicineMovement = _mapper.Map<List<object>>(records);
            return new Pager<object>(listMedicineMovement, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }
    }
}