using Microsoft.AspNetCore.Mvc;
using Social.Dto;
using Social.Models;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        await _userService.CreateUserAsync(user);
        return Ok("Usuario creado correctamente.");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userService.GetUsersAsync();
        return Ok(users);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto data)
    {
        var users = await _userService.GetUserLogin(data);
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
    {
        await _userService.UpdateUserAsync(id, user);
        return Ok("Usuario actualizado correctamente.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        return Ok("Usuario eliminado correctamente.");
    }



}