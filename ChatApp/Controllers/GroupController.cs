﻿using AutoMapper;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
[Authorize]
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
     [HttpGet("{id}")]
    public async Task<ReturnModel> GetById(int id){
        var message =await _groupService.GetByIdAsync(id);
        return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<List<GroupModel>>(message),
            StatusCode=200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] GroupCreateModel groupCreateModel){
        var group = _mapper.Map<Group>(groupCreateModel);
        var messageResult=await _groupService.AddAsync(group);
         return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<GroupModel>(messageResult),
            StatusCode=200
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] GroupUpdateModel groupUpdateModel){
        var group=_mapper.Map<Group>(groupUpdateModel);
        var groupResult=await _groupService.UpdateAsync(group);
         return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<GroupModel>(groupResult),
            StatusCode=200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id){
        var group =await _groupService.GetByIdAsync(id);
        await _groupService.DeleteAsync(group);
           return new ReturnModel{
            Success=true,
            Message="Merhaba",
            StatusCode=200
        };
    }

}
