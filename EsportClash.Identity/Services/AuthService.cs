using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EsportClash.Core.Shared;
using EsportClash.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EsportClash.Identity.Services;

public class AuthService {
  private readonly JwtSettings _jwtSettings;
  private readonly SignInManager<AppUser> _signInManager;
  private readonly UserManager<AppUser> _userManager;

  public AuthService(UserManager<AppUser> userManager, IOptions<JwtSettings> jwtSettings,
    SignInManager<AppUser> signInManager) {
    _userManager = userManager;
    _jwtSettings = jwtSettings.Value;
    _signInManager = signInManager;
  }

  public async Task<LoginResponse> Login(LoginRequest request) {
    var user = await _userManager.FindByEmailAsync(request.Email);
    if (user == null) throw new NotFoundException($"User with {request.Email} not found.", request.Email);

    var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
    if (result.Succeeded == false) throw new BadRequestException($"Credentials for '{request.Email} aren't valid'.");

    var jwtSecurityToken = await GenerateToken(user);

    var response = new LoginResponse {
      Id = user.Id,
      Email = user.Email,
      UserName = user.UserName,
      Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
    };

    return response;
  }

  public async Task<RegisterResponse> Register(RegisterRequest request) {
    var user = new AppUser {
      Email = request.Email,
      FirstName = request.FirstName,
      LastName = request.LastName,
      UserName = request.Email,
      EmailConfirmed = true
    };

    var result = await _userManager.CreateAsync(user, request.Password);
    if (result.Succeeded == false) {
      var errors = new StringBuilder();
      foreach (var error in result.Errors) errors.AppendLine(error.Description);

      throw new BadRequestException($"{errors}");
    }

    await _userManager.AddToRoleAsync(user, "User");

    return new RegisterResponse {
      UserId = user.Id
    };
  }

  private async Task<JwtSecurityToken> GenerateToken(AppUser user) {
    var userClaims = await _userManager.GetClaimsAsync(user);

    var roles = await _userManager.GetRolesAsync(user);
    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub, user.Id),
      new Claim(JwtRegisteredClaimNames.Email, user.Email)
    }.Union(userClaims).Union(roleClaims);

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    var jwt = new JwtSecurityToken(
      _jwtSettings.Issuer,
      _jwtSettings.Audience,
      claims,
      expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInMinutes),
      signingCredentials: credentials
    );

    return jwt;
  }
}