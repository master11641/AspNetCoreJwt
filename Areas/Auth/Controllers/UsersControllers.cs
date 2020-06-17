using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Area("Auth")]
[Route("Auth/[controller]")]
public class UsersController : ControllerBase {
    private IUserService _userService;

    public UsersController (IUserService userService) {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost ("authenticate")]
    // [Route("[action]")]
    public IActionResult Authenticate ([FromForm] AuthenticateRequest model) {
        var response = _userService.Authenticate (model);

        if (response == null)
            return BadRequest (new { message = "Username or password is incorrect" });

        return Ok (response);
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult GetAll () {
        var users = _userService.GetAll ();
        return Ok (users);
    }
 
}