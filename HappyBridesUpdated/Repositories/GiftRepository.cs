using Dapper;
using HappyBridesUpdated.Model;

namespace HappyBridesUpdated.Repositories;

public class GiftRepository
{
    private static ConnectRepository _connectRepository = new ConnectRepository();

    public List<Gift> GetGifts(int userId)
    {
        using var connection = _connectRepository.Connect();
        return connection.Query<Gift>("SELECT * FROM Gifts where idUsers = @userId", param: new{@userId = userId}).ToList();
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
}