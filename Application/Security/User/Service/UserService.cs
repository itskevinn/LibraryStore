using System;
using System.Threading.Tasks;
using Application.Security.User.Dto;
using Application.Security.User.Request;
using AutoMapper;
using Domain.Repository;
using Infrastructure.Security.Encrypt;

namespace Application.Security.User
{
  public class UserService
  {
    private readonly IGenericRepository<Domain.Entities.User> _userRepository;
    private readonly IMapper _mapper;

    public UserService(IGenericRepository<Domain.Entities.User> userService, IMapper mapper)
    {
      _userRepository = userService;
      _mapper = mapper;
    }

    public async Task<UserDto> Login(LoginRequest loginRequest)
    {
      var user = await _userRepository.FindAsync(
        u => u.UserName == loginRequest.UserName && u.Password == Hash.GetSha256(loginRequest.Password), false, null);
      if (user == null)
        throw new NullReferenceException("This username with this password are not registered on our system");
      var userDto = _mapper.Map<Domain.Entities.User, UserDto>(user);
      return userDto;
    }
  }
}