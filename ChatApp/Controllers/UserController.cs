using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]

public class UserController:Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
    {
        var users=await _userService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=users,
            StatusCode=200
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> Get(int id){
        var user=_userService.GetByIdAsync(id);
           return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=user,
            StatusCode=200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] User user){
        var newUser=await _userService.AddAsync(user);
         return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=user,
            StatusCode=200
            };
    }
}
