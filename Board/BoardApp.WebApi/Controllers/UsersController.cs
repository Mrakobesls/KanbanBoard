using AutoMapper;
using BoardApp.BLL.Services;
using BoardApp.Common.Models;
using BoardApp.WebApi.Exceptions;
using BoardApp.WebApi.Jwt;
using BoardApp.WebApi.Models;
using BoardApp.WebApi.Models.RequestModels;
using BoardApp.WebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BoardApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper, ITokenService tokenService)
        {
            _userService = userService;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult Register(RegisterUserRequest request)
        {
            var dto = _mapper.Map<UserDto>(request);
            try
            {
                _userService.Add(dto);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            var loginRequest = new LoginUserRequest()
            {
                LoginOrEmail = request.Login,
                Password = request.Password
            };

            return Login(loginRequest);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginUserRequest request)
        {
            var user = _userService.Authenticate(request.LoginOrEmail, request.Password);
            var response = new LoginUserResponse { IsSuccess = user is not null };

            if (response.IsSuccess)
            {
                var token = _tokenService.GenerateToken(user);
                response.Token = token;

                return Ok(response);
            }

            return Unauthorized(response);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id = 0)
        {
            try
            {
                if (id == 0)
                    id = _tokenService.GetUserId(HttpContext);
            }
            catch (AuthorizeException e)
            {
                NotFound(e.Message);
            }
            catch (Exception e)
            {
                NotFound(e.Message);
            }

            var user = _userService.Read(id);
            var response = _mapper.Map<UserModel>(user);

            return Ok(response);
        }
    }
}
