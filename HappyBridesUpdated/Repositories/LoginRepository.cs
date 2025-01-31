using Dapper;

namespace HappyBridesUpdated.Repositories;

public class LoginRepository
{
    private static ConnectRepository _repository = new ConnectRepository();

    public bool Login(string email, string password)
    {
        using var connection = _repository.Connect();

        if (connection.QuerySingle<bool>("SELECT count(1) FROM Users WHERE email = @email AND password = @password", param: new {@email = email, @password = password}))
        {
            return true;
        }
        return false;
    }
}