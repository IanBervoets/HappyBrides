using System;
using System.Linq;
using Dapper;

namespace HappyBridesUpdated.Repositories;

public class RegisterRepository
{
    public static ConnectRepository Repository = new ConnectRepository();
    
    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static int RegisterAccount(string name, string email, int isBride, string passWD, string keyString)
    {
        using var connection = Repository.Connect();
        
        connection.Execute("INSERT INTO Users (email, password, name, isBride, keyString) VALUES (@email, @passWD, @name, @isBride, @keyString)", 
            param: new {@email = email, @passWD = passWD, @name = name, @isBride = isBride, @keyString = keyString});
        
        var userId = connection.QuerySingle<int>("SELECT idUsers FROM Users ORDER BY idUsers DESC LIMIT 1;");

        return userId; 
    }

    public static bool EmailNotRegistered(string email)
    {
        using var connection = Repository.Connect();
        return connection.QuerySingle<bool>("SELECT count(1) FROM Users WHERE email = @email",
            param: new {@email = email});
    }
    
    public static bool CheckKey(string keyString)
    {
        using var connection = Repository.Connect();
        return (connection.QuerySingle<bool>("SELECT count(1) FROM Users WHERE keyString = @keyString",
            param: new {@keyString = keyString}));
    }

}