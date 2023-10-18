using API.Dtos;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
    public class EndpointsController: ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public EndpointsController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        //Requerimientos grupo A
        [HttpGet("cardiovascularSurgeonVeterinarian")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VeterinarianDto>>> Get(int id)
        {
            var veterinarians = await _unitOfwork.Veterinarians.GetCardiovascularSurgeonAsync();
            if (veterinarians == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<VeterinarianDto>>(veterinarians);
        }
        [HttpGet("medicineBy{laboratory}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<VeterinarianDto>>> Get2(string laboratory)
        {
            var veterinarians = await _unitOfwork.Veterinarians.GetCardiovascularSurgeonAsync();
            if (veterinarians == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<VeterinarianDto>>(veterinarians);
        }

    }