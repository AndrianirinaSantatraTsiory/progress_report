using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgressReport.DTO;
using ProgressReport.Services;

namespace ProgressReport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("directions")]
    public class DirectionController : ControllerBase
    {
       private readonly IDirectionService _directionService;

        public DirectionController(IDirectionService directionService)
        {
            _directionService = directionService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<DirectionDto>>>> GetAll()
        {
            var services = await _directionService.GetAll();
            return Ok(new ResponseModel<IEnumerable<DirectionDto>>("Services récupérés avec succès", services));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ResponseModel<DirectionDto>>> GetById(long id)
        {
            var service = await _directionService.GetById(id);
            return Ok(new ResponseModel<DirectionDto>("Direction récupéré avec succès", service));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel<DirectionDto>>> Create([FromBody] RequestDirectionDto dto)
        {
            var createdService = await _directionService.Create(dto);

            var response = new ResponseModel<DirectionDto>("Direction créé avec succès", createdService);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ResponseModel<DirectionDto>>> Update(long id, [FromBody] RequestDirectionDto dto)
        {
            var updatedService = await _directionService.Update(id, dto);

            var response = new ResponseModel<DirectionDto>("Direction mis à jour avec succès", updatedService);
            return Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ResponseModel<object?>>> Delete(long id)
        {
            await _directionService.Delete(id);

            var response = new ResponseModel<object?>("Direction supprimé avec succès",null);
            return Ok(response);
        } 
    }
}