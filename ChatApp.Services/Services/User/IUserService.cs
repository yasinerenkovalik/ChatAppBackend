using ChatApp.Data;

namespace ChatApp.Services;

public interface IUserService:IGenericService<User>
{
    public Task<User> GetByUserNameAndPasswordAsync(string username,string password);

}
