using ChatApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Data;

public static class Register
{
    public static void RegisterServices(this IServiceCollection services){
        services.AddScoped<IUserService ,UserService>();
        services.AddScoped<IGroupService ,GroupService>();
        services.AddScoped<IMessageService ,MessageService>();
    }

}