using Core.DTO;
using Core.Identity;
using System.Security.Claims;

namespace Core.ServiceContracts
{
 public interface IJwtService
 {
  AuthenticationResponse CreateJwtToken(ApplicationUser user);
  ClaimsPrincipal? GetPrincipalFromJwtToken(string? token);
 }
}
