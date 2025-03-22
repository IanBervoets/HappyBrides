using Dapper;

namespace HappyBridesUpdated.Repositories;

public class UserRepository
{
    private static ConnectRepository _connectRepository = new ConnectRepository();

    public void ChangeBrideStatus(int userId, int isBride)
    {
        using var connection = _connectRepository.Connect();
        connection.Query("UPDATE Users SET isBride = @isBride WHERE idUsers = @idUsers;", param: new {idUsers = userId, isBride = isBride});
    }

    public string getBrideName(string key)
    {
        using var connection = _connectRepository.Connect();
        return connection.QuerySingle<string>("SELECT name FROM Users WHERE keyString = @key", param: new { @key = key });
    }
}