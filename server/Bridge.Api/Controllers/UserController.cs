using Bridge.Core.Models;
using Bridge.Core.Resources;
using Bridge.Core.Services;
using Bridge.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bridge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }
        [HttpPost]
        public async Task<ActionResult<Guid>> Add(UserResource entity)
        {
            return Ok(await _userService.Add(entity));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteById(Guid id)
        {
            return Ok(await _userService.DeleteById(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResource?>> GetById(Guid id)
        {
            var user = await _userService.GetById(id);
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> LogIn([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var user = await _userService.LogIn(email, password);
                if (user == null)
                {
                    return Unauthorized("Invalid email or password");
                }

                var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name);
                return Ok(new AuthResponse { User = user, Token = token });
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] UserRegisterResource registerResource)
        {
            try
            {
                var user = await _userService.Register(registerResource);
                if (user == null)
                {
                    return Conflict("User already exists");
                }

                var token = _tokenService.GenerateToken(user.Id, user.Email, user.Name);
                return Ok(new AuthResponse { User = user, Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nearby-users")]
        public async Task<ActionResult<IEnumerable<UserResource>>> GetUsersInRadius([FromQuery] double lat, [FromQuery] double lng, [FromQuery] double radiusInMeters)
        {
            var users = await _userService.GetUsersInRadius(lat, lng, radiusInMeters);
            return Ok(users);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<int>> UpdateUser([FromBody] UserResource userResource)
        {
            return Ok(await _userService.Update(userResource));
        }

        [Authorize]
        [HttpPut("update-location")]
        public async Task<ActionResult<int>> UpdateLocation([FromBody] UpdateLocationResource updateLocation)
        {
            return Ok(await _userService.UpdateLocation(updateLocation.Id, updateLocation.Lat, updateLocation.Lng));
        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserResource resource)
        {
            var result = await _userService.UpdateProfile(resource);

            if (result == null) return NotFound("User not found");

            return Ok(result);
        }
    }
}