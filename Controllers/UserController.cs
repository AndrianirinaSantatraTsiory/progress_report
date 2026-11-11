using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgressReport.DTO;
using ProgressReport.Services;

namespace ProgressReport.Controllers
{
    [Authorize]
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<ActionResult<ResponseModel<IEnumerable<UserDto>>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(new ResponseModel<IEnumerable<UserDto>>("Utilisateurs récupérés avec succès",users));
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ResponseModel<UserDto>>> GetById(long id)
        {
            var user = await _userService.GetById(id);
            return Ok(new ResponseModel<UserDto>("Utilisateur récupéré avec succès",user));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        public async Task<ActionResult<ResponseModel<UserDto>>> Create([FromBody] RequestUserDto dto)
        {
            var createdUser = await _userService.Create(dto);

            var response = new ResponseModel<UserDto>(
                Message: "Utilisateur créé avec succès",
                Data: createdUser
            );

            return StatusCode(StatusCodes.Status201Created, new ResponseModel<UserDto>("Utilisateur créé avec succès", createdUser));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("{id:long}")]
        public async Task<ActionResult<ResponseModel<UserDto>>> Update(long id, [FromBody] RequestUserDto dto)
        {
            var updatedUser = await _userService.Update(id, dto);

            var response = new ResponseModel<UserDto>(
                Message: "Utilisateur mis à jour avec succès",
                Data: updatedUser
            );
            return Ok(response);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<ResponseModel<object?>>> Delete(long id)
        {
            await _userService.Delete(id);
           

            var response = new ResponseModel<object?>(
                Message: "Utilisateur supprimé avec succès",
                Data: null
            );
            return Ok(response);
        }        
    }
}