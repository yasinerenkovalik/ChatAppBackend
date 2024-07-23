using AutoMapper;
using Azure.Core;
using ChatApp.Data;
using ChatApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MessageController:Controller
{
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public MessageController(IMessageService messageService,IMapper mapper)
    {
        _messageService = messageService;
        _mapper=mapper;
    }
    [HttpGet]
    public async Task<ReturnModel> Get([FromQuery]PaginationModel paginationModel){
        var message =await _messageService.ListAllAsync(paginationModel);
        return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<List<MessageModel>>(message),
            StatusCode=200,
            TotalCount=await _messageService.CountAsync()
        };
    }
    [HttpGet("{id}")]
    public async Task<ReturnModel> GetById(int id){
        var message =await _messageService.GetByIdAsync(id);
        return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<List<MessageModel>>(message),
            StatusCode=200
        };
    }
    [HttpPost]
    public async Task<ReturnModel> Post([FromBody] MessageCreateModel messageCreateModel){
        var message = _mapper.Map<Message>(messageCreateModel);
        var messageResult=await _messageService.AddAsync(message);
         return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<MessageModel>(messageResult),
            StatusCode=200
        };
    }
    [HttpPut]
    public async Task<ReturnModel> Put([FromBody] MessageUpdateModel messageUpdateModel){
        var message=_mapper.Map<Message>(messageUpdateModel);
        var messageResult=await _messageService.UpdateAsync(message);
         return new ReturnModel{
            Success=true,
            Message="Merhaba",
            Data=_mapper.Map<MessageModel>(messageResult),
            StatusCode=200
        };
    }
    [HttpDelete("{id}")]
    public async Task<ReturnModel> Delete(int id){
        var message =await _messageService.GetByIdAsync(id);
        await _messageService.DeleteAsync(message);
           return new ReturnModel{
            Success=true,
            Message="Merhaba",
            StatusCode=200
        };
        

    }
}
