using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp;
[Route("api/[controller]")]
[ApiController]

public class HubController:Controller
{
    private readonly ISignalrConnection _signalarConncetion;

    public HubController(ISignalrConnection signalarConncetion)
    {
        _signalarConncetion = signalarConncetion;
    }
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] string message){
        var connect=_signalarConncetion.StartConnection();
        await connect.InvokeAsync("SendMessageToAll","Admin",message);
        return Ok();
    }
}
