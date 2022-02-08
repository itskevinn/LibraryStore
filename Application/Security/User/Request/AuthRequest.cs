using System.ComponentModel.DataAnnotations;

namespace Application.Security.User.Request
{
  public record AuthRequest([Required] string UserName, [Required] string Password);
}