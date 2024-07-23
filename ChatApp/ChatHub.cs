﻿using ChatApp.Data;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp;

public class ChatHub:Hub
{
    public override async Task OnConnectedAsync(){
        await Clients.Caller.SendAsync("UserConnected",Context.ConnectionId);
    }
    public  async Task SendMessageToAll(string user ,string message){
        await Clients.All.SendAsync("ReceiveMessage",user,message);
    }
    public  async Task SendMessageToUser(string connectionId ,string message){
        await Clients.User(connectionId).SendAsync("ReceiveMessage",message);
    }
    public  async Task SendMessageToGroup(string group ,string message){
        await Clients.Group(group).SendAsync("ReceiveMessage",message);
    }
    
    


}
