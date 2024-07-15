using AutoMapper;
using ChatApp.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChatApp.Services;

public class MyProfiles:Profile
{
    public MyProfiles()
    {
        CreateMap<User,UserCreateModel>().ReverseMap();
        CreateMap<User,UserModel>().ReverseMap();
        CreateMap<User,UserUpdateModel>().ReverseMap();
        CreateMap<Message,MessageModel>().ReverseMap();
        CreateMap<Message,MessageCreateModel>().ReverseMap();
        CreateMap<Message,MessageUpdateModel>().ReverseMap();
    }

}
