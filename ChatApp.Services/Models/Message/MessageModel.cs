﻿
namespace ChatApp.Services;

public class MessageModel
{
    public string Content { get; set; }
    public int? UserId { get; set; }
    public int? GroupId { get; set; }
}
