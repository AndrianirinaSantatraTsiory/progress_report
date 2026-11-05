using Microsoft.AspNetCore.Mvc;
using ProgressReport.DTO;
using ProgressReport.Services;

namespace ProgressReport.Controllers
{
    [ApiController]
    [Route("services")]
    public class ServiceController : ControllerBase
    {
       private readonly IService _serviceService;

        public ServiceController(IService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<ServiceDto>>>> GetAll()
        {
            var services = await _serviceService.GetAll();
            return Ok(new ResponseModel<IEnumerable<ServiceDto>>("Services récupérés avec succès", services));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ResponseModel<ServiceDto>>> GetById(long id)
        {
            var service = await _serviceService.GetById(id);
            return Ok(new ResponseModel<ServiceDto>("Service récupéré avec succès", service));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<ServiceDto>>> Create([FromBody] RequestServiceDto dto)
        {
            var createdService = await _serviceService.Create(dto);

            var response = new ResponseModel<ServiceDto>("Service créé avec succès", createdService);

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult<ResponseModel<ServiceDto>>> Update(long id, [FromBody] RequestServiceDto dto)
        {
            var updatedService = await _serviceService.Update(id, dto);

            var response = new ResponseModel<ServiceDto>("Service mis à jour avec succès", updatedService);
            return Ok(response);
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ResponseModel<object?>>> Delete(long id)
        {
            await _serviceService.Delete(id);

            var response = new ResponseModel<object?>("Service supprimé avec succès",null);
            return Ok(response);
        } 
    }
}