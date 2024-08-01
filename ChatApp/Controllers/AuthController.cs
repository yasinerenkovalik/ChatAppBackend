using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]

public class AuthController:Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;



    public AuthController(IUserService userService,IConfiguration configuration,IMapper mapper)
    {
        _userService = userService;
        _configuration=configuration;
        _mapper=mapper;
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
    [HttpPost("register")]
    public async Task<ReturnModel> Register([FromBody] UserCreateModel userCreateModel){
        var newUserr=_mapper.Map<User>(userCreateModel);
        var newUser=await _userService.AddAsync(newUserr);
         return new ReturnModel{
                Success=true,
                Message="User created succesfull",
                StatusCode=200,
                Data=newUser
            };
    }

}
