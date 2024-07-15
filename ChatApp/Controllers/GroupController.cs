using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
public class GroupController:Controller
{
    private readonly IGroupService _groupService;
    private readonly IMapper _mapper;

    public GroupController(IGroupService groupService,IMapper mapper)
    {
        _groupService = groupService;
        _mapper=mapper;
    }
     [HttpGet]
    public async Task<ReturnModel> Get([FromQuery]PaginationModel paginationModel){
        var message =await _groupService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<List<GroupModel>>(message),
            StatusCode=200,
            TotalCount=await _groupService.CountAsync()
        };
    }

}
