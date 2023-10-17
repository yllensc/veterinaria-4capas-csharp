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
    public class ProviderController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;

        public ProviderController(IUnitOfWork unitofwork, IMapper mapper)
        {
            this._unitOfwork = unitofwork;
            this._mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProviderDto>>> Get()
        {
            var Provider = await _unitOfwork.Providers.GetAllAsync();
            return _mapper.Map<List<ProviderDto>>(Provider);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProviderDto>> Get(int id)
        {
            var Provider = await _unitOfwork.Providers.GetByIdAsync(id);
            if (Provider == null)
            {
                return NotFound();
            }
            return this._mapper.Map<ProviderDto>(Provider);
        }

        [HttpGet]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ProviderDto>>> GetPagination([FromQuery] Params Params)
        {
            var (totalRecords, records) = await _unitOfwork.Providers.GetAllAsync(Params.PageIndex, Params.PageSize, Params.Search);
            var listProvider = _mapper.Map<List<ProviderDto>>(records);
            return new Pager<ProviderDto>(listProvider, totalRecords, Params.PageIndex, Params.PageSize, Params.Search);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Provider>> Post(ProviderDto ProviderDto)
        {
            var Provider = this._mapper.Map<Provider>(ProviderDto);
            this._unitOfwork.Providers.Add(Provider);
            await _unitOfwork.SaveAsync();
            if (Provider == null)
            {
                return BadRequest();
            }
            ProviderDto.Id = Provider.Id;
            return CreatedAtAction(nameof(Post), new { id = ProviderDto.Id }, ProviderDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProviderDto>> Put(int id, [FromBody]ProviderDto ProviderDto)
        {
            if (ProviderDto == null)
            {
                return NotFound();
            }
            var Provider = this._mapper.Map<Provider>(ProviderDto);
            _unitOfwork.Providers.Update(Provider);
            await _unitOfwork.SaveAsync();
            return ProviderDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var Provider = await _unitOfwork.Providers.GetByIdAsync(id);
            if (Provider == null)
            {
                return NotFound();
            }
            _unitOfwork.Providers.Remove(Provider);
            await _unitOfwork.SaveAsync();
            return NoContent();
        }
    }
}