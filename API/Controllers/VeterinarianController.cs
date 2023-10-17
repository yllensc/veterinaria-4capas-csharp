using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace API.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [Authorize]
    public class VeterinarianController : ApiBaseController
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IMapper mapper;

        public VeterinarianController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this.unitofwork = unitofwork;
            this.mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<VeterinarianDto>>> Get()
        {
            var Veterinarian = await unitofwork.Veterinarians.GetAllAsync();
            return mapper.Map<List<VeterinarianDto>>(Veterinarian);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeterinarianDto>> Get(int id)
        {
            var Veterinarian = await unitofwork.Veterinarians.GetByIdAsync(id);
            if (Veterinarian == null)
            {
                return NotFound();
            }
            return this.mapper.Map<VeterinarianDto>(Veterinarian);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<VeterinarianDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await unitofwork.Veterinarians.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listVeterinarian = mapper.Map<List<VeterinarianDto>>(records);
            return new Pager<VeterinarianDto>(listVeterinarian, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Veterinarian>> Post(VeterinarianDto VeterinarianDto)
        {
            var veterinarian = this.mapper.Map<Veterinarian>(VeterinarianDto);
            this.unitofwork.Veterinarians.Add(veterinarian);
            await unitofwork.SaveAsync();
            if (veterinarian == null)
            {
                return BadRequest();
            }
            VeterinarianDto.Id = veterinarian.Id;
            return CreatedAtAction(nameof(Post), new { id = VeterinarianDto.Id }, VeterinarianDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VeterinarianDto>> Put(int id, [FromBody]VeterinarianDto veterinarianDto)
        {
            if (veterinarianDto == null)
            {
                return NotFound();
            }
            var veterinarian = this.mapper.Map<Veterinarian>(veterinarianDto);
            unitofwork.Veterinarians.Update(veterinarian);
            await unitofwork.SaveAsync();
            return veterinarianDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var veterinarian = await unitofwork.Veterinarians.GetByIdAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            unitofwork.Veterinarians.Remove(veterinarian);
            await unitofwork.SaveAsync();
            return NoContent();
        }
    }
}