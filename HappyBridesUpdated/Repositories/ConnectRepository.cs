using System.Data;
using MySqlConnector;

namespace HappyBridesUpdated.Repositories;

public class ConnectRepository
{
    public IDbConnection Connect()
    {
        return new MySqlConnection(
            "Server=localhost;" +
            "Port=3306;" +
            "Database=HappyBrides;" +
            "Uid=root;" +
            "Pwd=root;"
        );
    }
}

