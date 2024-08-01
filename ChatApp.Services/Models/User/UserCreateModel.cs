﻿namespace ChatApp.Services;

public record class UserCreateModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FullName { get; set; }

}
