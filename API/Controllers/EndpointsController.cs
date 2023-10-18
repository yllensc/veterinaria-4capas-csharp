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
        public async Task<ActionResult<LaboratoryWithMedicinesDto>> Get2(string laboratory)
        {
            var laboratories = await _unitOfwork.Laboratories.GetMedicines(laboratory);
            if (laboratories == null)
            {
                return NotFound();
            }
            return this._mapper.Map<LaboratoryWithMedicinesDto>(laboratories);
        }
        [HttpGet("petsBySpecie{specie}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SpecieWithPetsDto>> Get3(string specie)
        {
            var species = await _unitOfwork.Species.GetPets(specie);
            if (species == null)
            {
                return NotFound();
            }
            return this._mapper.Map<SpecieWithPetsDto>(species);
        }
        [HttpGet("ownersWithPets")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<OwnerWithPetsDto>>> Get4()
        {
            var owners = await _unitOfwork.Owners.GetAllWithPetsAsync();
            if (owners == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<OwnerWithPetsDto>>(owners);
        }
        [HttpGet("medicineWithLessThan{cant}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicineBasicDto>>> Get5(int cant)
        {
            var medicines = await _unitOfwork.Medicines.GetUnderCant(cant);
            if (medicines == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<MedicineBasicDto>>(medicines);
        }
        [HttpGet("petsOn{year}On{quarter}for{cause}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AppointmentWithPets>>> Get6(string cause, int quarter, int year)
        {
            var appointments = await _unitOfwork.Appointments.GetPetsByAppointmentEspecific(cause, quarter, year);
            if (appointments == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<AppointmentWithPets>>(appointments);
        }
        //Requerimientos grupo B
        [HttpGet("speciesOnGroups")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SpeciesAllWithPetsDto>>> Get7()
        {
            var species = await _unitOfwork.Species.GetPetsInGroups();
            if (species == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<SpeciesAllWithPetsDto>>(species);
        }
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
        [HttpGet("petsCaredByVeterinarian{IdVeterinarian}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<AppointmentWithPets>>> Get9(int IdVeterinarian)
        {
            var appointments = await _unitOfwork.Appointments.GetPetsOnAppointmentWithVeterinarianX(IdVeterinarian);
            if (appointments == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<AppointmentWithPets>>(appointments);
        }
        [HttpGet("providerWithThis{medicine}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MedicineJustProvidersDto>>> Get10(string medicine)
        {
            var medicines = await _unitOfwork.Medicines.GetProvidersWithXMedicine(medicine);
            if (medicines == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<MedicineJustProvidersDto>>(medicines);
        }
        [HttpGet("petsWithThis{race}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PetsBasicWithOwnerDto>>> Get11(string race)
        {
            var pets = await _unitOfwork.Pets.GetPetsWithXRace(race);
            if (pets == null)
            {
                return NotFound();
            }
            return this._mapper.Map<List<PetsBasicWithOwnerDto>>(pets);
        }

    }