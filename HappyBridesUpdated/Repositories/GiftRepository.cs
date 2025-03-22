using Dapper;
using HappyBridesUpdated.Model;

namespace HappyBridesUpdated.Repositories;

public class GiftRepository
{
    private static ConnectRepository _connectRepository = new ConnectRepository();

    public List<Gift> GetOwnGifts(int userId)
    {
        using var connection = _connectRepository.Connect();
        return connection.Query<Gift>("SELECT * FROM Gifts where idUsers = @userId", param: new{@userId = userId}).ToList();
    }

    public List<Gift> GetOtherGifts(string key)
    {
        using var connection = _connectRepository.Connect();
        int id = connection.QuerySingle<int>("SELECT idUsers FROM Users where keyString = @key", param: new {@key = key});
        return connection.Query<Gift>("SELECT * FROM Gifts where idUsers = @id", param: new{@id = id}).ToList();
    }
    
    public void AddGift(Gift gift)
    {
        using var connection = _connectRepository.Connect();
        connection.Execute("INSERT INTO Gifts (idUsers, name, priority, isBought) VALUES ((select idUsers from Users where idUsers = @userId), @name, @priority, @isBought)",param: new {@userId = gift.idUsers, gift.Name, gift.Priority, gift.IsBought});
    }

    public void RemoveGift(int id)
    {
        using var connection = _connectRepository.Connect();
        connection.Execute("DELETE FROM Gifts WHERE idGifts = @idGifts", param: new{@idGifts = id});
    }

    public void UpdateGift(Gift gift)
    {
        using var connection = _connectRepository.Connect();
        connection.Execute("UPDATE Gifts SET priority = @priority WHERE idGifts = @idGifts AND idUsers = @idUsers", param: new{@priority = gift.Priority, @idGifts = gift.idGifts, @idUsers = gift.idUsers});
    }

    public bool OwnsList(string key, int id)
    {
        using var connection = _connectRepository.Connect();
        if (connection.QuerySingle("SELECT COUNT(1) FROM Users WHERE keyString = @key AND idUsers = @id", param: new {@key = key, @id = id}) != null)
        {
            return true;
        }
        return false;
    }
}