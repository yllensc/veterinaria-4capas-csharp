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
    public class MedicineController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public MedicineController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicineDto>>> Get()
        {
            var Medicine = await _unitOfwork.Medicines.GetAllAsync();
            return _mapper.Map<List<MedicineDto>>(Medicine);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicineDto>> Get(int id)
        {
            var Medicine = await _unitOfwork.Medicines.GetByIdAsync(id);
            if (Medicine == null)
            {
                return NotFound();
            }
            return this._mapper.Map<MedicineDto>(Medicine);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<MedicineDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Medicines.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listMedicine = _mapper.Map<List<MedicineDto>>(records);
            return new Pager<MedicineDto>(listMedicine, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Medicine>> Post(MedicineDto MedicineDto)
        {
            var Medicine = this._mapper.Map<Medicine>(MedicineDto);
            this._unitOfwork.Medicines.Add(Medicine);
            await _unitOfwork.SaveAsync();
            if (Medicine == null)
            {
                return BadRequest();
            }
            MedicineDto.Id = Medicine.Id;
            return CreatedAtAction(nameof(Post), new { id = MedicineDto.Id }, MedicineDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MedicineDto>> Put(int id, [FromBody]MedicineDto MedicineDto)
        {
            if (MedicineDto == null)
            {
                return NotFound();
            }
            var Medicine = this._mapper.Map<Medicine>(MedicineDto);
            _unitOfwork.Medicines.Update(Medicine);
            await _unitOfwork.SaveAsync();
            return MedicineDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Medicine = await _unitOfwork.Medicines.GetByIdAsync(id);
            if (Medicine == null)
            {
                return NotFound();
            }
            _unitOfwork.Medicines.Remove(Medicine);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}