namespace ChatApp.Services;

public record class UserCreateModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Emanil { get; set; }
    public string FullName { get; set; }

}
