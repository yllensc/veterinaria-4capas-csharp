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
    public class LaboratoryController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public LaboratoryController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<LaboratoryDto>>> Get()
        {
            var Laboratory = await _unitOfwork.Laboratories.GetAllAsync();
            return _mapper.Map<List<LaboratoryDto>>(Laboratory);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaboratoryDto>> Get(int id)
        {
            var Laboratory = await _unitOfwork.Laboratories.GetByIdAsync(id);
            if (Laboratory == null)
            {
                return NotFound();
            }
            return this._mapper.Map<LaboratoryDto>(Laboratory);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<LaboratoryDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Laboratories.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listLaboratory = _mapper.Map<List<LaboratoryDto>>(records);
            return new Pager<LaboratoryDto>(listLaboratory, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Laboratory>> Post(LaboratoryDto LaboratoryDto)
        {
            var Laboratory = this._mapper.Map<Laboratory>(LaboratoryDto);
            this._unitOfwork.Laboratories.Add(Laboratory);
            await _unitOfwork.SaveAsync();
            if (Laboratory == null)
            {
                return BadRequest();
            }
            LaboratoryDto.Id = Laboratory.Id;
            return CreatedAtAction(nameof(Post), new { id = LaboratoryDto.Id }, LaboratoryDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LaboratoryDto>> Put(int id, [FromBody] LaboratoryDto LaboratoryDto)
        {
            if (LaboratoryDto == null)
            {
                return NotFound();
            }
            var Laboratory = this._mapper.Map<Laboratory>(LaboratoryDto);
            _unitOfwork.Laboratories.Update(Laboratory);
            await _unitOfwork.SaveAsync();
            return LaboratoryDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Laboratory = await _unitOfwork.Laboratories.GetByIdAsync(id);
            if (Laboratory == null)
            {
                return NotFound();
            }
            _unitOfwork.Laboratories.Remove(Laboratory);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
        //Endpoints
        [HttpGet("medicineBy{laboratory}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LaboratoryWithMedicinesDto>> Get2(string laboratory)
        {
            var laboratories = await _unitOfwork.Laboratories.GetMedicines(laboratory);
            if (laboratories == null)
            {
                return NotFound();
            }
            return this._mapper.Map<LaboratoryWithMedicinesDto>(laboratories);
        }
        [HttpGet("medicineBy{laboratory}")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<LaboratoryWithMedicinesDto>>> GetPaginationEnd2([FromQuery] Params Params, string laboratory)
        {
            var (totalRecords, records) = await _unitOfwork.Laboratories.GetMedicines(laboratory, Params.PageIndex, Params.PageSize, Params.Search);
            var listLaboratory = _mapper.Map<List<LaboratoryWithMedicinesDto>>(records);
            return new Pager<LaboratoryWithMedicinesDto>(listLaboratory, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }
    }
}