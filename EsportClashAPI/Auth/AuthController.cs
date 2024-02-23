using EsportClash.Identity.Models;
using EsportClash.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsportClashAPI.Auth;

[ApiController]
[Route("/auth")]
public class AuthController : ControllerBase {
  private readonly AuthService _authService;

  public AuthController(AuthService authService) {
    _authService = authService;
  }

  [HttpPost("login")]
  public async Task<ActionResult<LoginResponse>> Login(LoginRequest request) {
    return Ok(await _authService.Login(request));
  }

  [HttpPost("register")]
  public async Task<ActionResult<RegisterResponse>> Register(RegisterRequest request) {
    return Ok(await _authService.Register(request));
  }
}