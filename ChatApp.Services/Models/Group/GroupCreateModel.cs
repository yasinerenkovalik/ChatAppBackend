﻿namespace ChatApp.Services;

public record class GroupCreateModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Avatar { get; set; }

}
