using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AuthController:Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService,IConfiguration configuration)
    {
        _userService = userService;
        _configuration=configuration;
    }

    [HttpPost("login")]

    public async Task<ReturnModel> Login([FromBody]LoginModel loginModel)
    {
        var user=await _userService.GetByUserNameAndPasswordAsync(loginModel.UserName,loginModel.Password);

        if(user==null){
            return new ReturnModel{
                Success=false,
                Message="invalid username or password",
                StatusCode=400
            };
        }
        var tokenModel=new TokenModel{
            UserName=user.Username,
            Role=user.Role,
            SigninKey=_configuration["Jwt:SigningKey"],
            Issuer=_configuration["Jwt:Issure"],
            Audience=_configuration["Jwt:Audience"],
        };
        var token =JwtToken.GenerateToke(tokenModel);
         return new ReturnModel{
                Success=true,
                Message="Login Succesful",
                StatusCode=200,
                Data=token
            };


    }

}
