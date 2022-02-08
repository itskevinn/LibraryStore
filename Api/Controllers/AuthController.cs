using System.Threading.Tasks;
using Application.Security.User.Request;
using Application.Security.User.Service;
using Application.Utilities;
using Domain.Entities;
using Domain.Repository;
using Infrastructure.Security.JwtService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class AuthController
  {
    private readonly UserService _userService;

    public AuthController(IGenericRepository<UsersRoles> repository,
      IGenericRepository<Role> roleRepository, TokenService tokenService)
    {
      _userService ??= new UserService(repository, roleRepository, tokenService);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<Response<string>>> Post(AuthRequest authRequest) =>
      await _userService.Login(authRequest);
  }
}