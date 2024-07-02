using dotnet6_rpg.Data;
using dotnet6_rpg.Dtos.User;
using Microsoft.AspNetCore.Mvc;

namespace dotnet6_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
            
        }
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponce<int>>>Resister(UserRegisterDto request)
        {
            var responce=await _authRepo.Register(
                new User{UserName=request.UserName},request.Password
            );
            if(!responce.Sucess)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponce<string>>>Login(UserLoginDto request)
        {
            var responce=await _authRepo.Login(request.UserName,request.password);
                
            if(!responce.Sucess)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}