using Dapper;
using HappyBridesUpdated.Model;

namespace HappyBridesUpdated.Repositories;

public class LoginRepository
{
    private static ConnectRepository _repository = new ConnectRepository();

    public bool CheckAccount(string email, string password)
    {
        using var connection = _repository.Connect();
        if (connection.QuerySingle<bool>("SELECT count(1) FROM Users WHERE email = @email AND password = @password", param: new{ @email, @password}))
        {
            return true;
        }
        return false;
    }
    
    public User GetUser(string email, string password)
    {
        User user = new User();
        using var connection = _repository.Connect();
        user.Id = connection.QuerySingle<int>("SELECT idUsers FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        user.Email = connection.QuerySingle<string>("SELECT email FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        user.Password = connection.QuerySingle<string>("SELECT password FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        user.Name = connection.QuerySingle<string>("SELECT name FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        user.IsBride = connection.QuerySingle<bool>("SELECT isBride FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        user.KeyString = connection.QuerySingle<string>("SELECT keyString FROM Users WHERE email = @email AND password = @password",
            param: new { email, password });
        return user;        
    }
}