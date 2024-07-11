using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]

public class UserController:Controller
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery] PaginationModel paginationModel)
    {
        var users=await _userService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=_mapper.Map<List<UserModel>>(users),
            StatusCode=200,
            TotalCount= await _userService.CountAsync()
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
    public async Task<ReturnModel> Post([FromBody] UserCreateModel userCreateModel){
        var newUserr=_mapper.Map<User>(userCreateModel);
        var newUser=await _userService.AddAsync(newUserr);
         return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=newUser,
            StatusCode=200,
        
            };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] UserUpdateModel userUpdateModel){
        var user =_mapper.Map<User>(userUpdateModel);
        var updateUser=await _userService.UpdateAsync(user);
            return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=_mapper.Map<UserModel>(updateUser),
            StatusCode=200,
        
            };


    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id){
        var user = await _userService.GetByIdAsync(id);
        await _userService.DeleteAsync(user);
           return new ReturnModel{
            Success=true,
            Message="user fetched successfuly",
            Data=_mapper.Map<UserModel>(user),
            StatusCode=200,
            };



    }
}
