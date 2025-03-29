using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[Route("/auth")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly AuthService authService;
    public AuthController(AuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var token = await authService.Authentication(loginDto);
        if (token == null) return Unauthorized();
        return Ok(new { Token = token });
    }
}