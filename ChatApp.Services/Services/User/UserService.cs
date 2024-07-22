using ChatApp.Data;

namespace ChatApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> AddAsync(User entity)
    {
        return await _userRepository.AddAsync(entity);
    }

    public async Task<int> CountAsync()
    {
        return await _userRepository.CountAsync();
    }

    public async Task<User> DeleteAsync(User entity)
    {
        return await _userRepository.DeleteAsync(entity);
    }

    public async Task<User> GetByIdAsync(int id)
    {

        var user = await _userRepository.GetByIdAsync(id);
        return user;
    }

    public async Task<IReadOnlyList<User>> ListAllAsync(PaginationModel paginationModel)
    {
        return await _userRepository.ListAllAsync(paginationModel);
    }

    public async Task<User> UpdateAsync(User entity)
    {
        return await _userRepository.UpdateAsync(entity);
    }
    public async Task<User> GetByUserNameAndPasswordAsync(string username,string password)
    {
        return await _userRepository.GetByUserNameAndPasswordAsync(username,password);

    }
}
