using Dapper;
using HappyBridesUpdated.Model;

namespace HappyBridesUpdated.Repositories;

public class GiftRepository
{
    private static ConnectRepository _connectRepository = new ConnectRepository();

    public List<Gift> GetGifts()
    {
        using var connection = _connectRepository.Connect();
        return connection.Query<Gift>("SELECT * FROM Gifts").ToList();
    }
    
    public void AddGift(Gift gift)
    {
        using var connection = _connectRepository.Connect();
        connection.Execute("INSERT INTO Gift (idUsers, name, priority, isBought) VALUES (@idUsers, @name, @priority, @isBought)",param: new {gift.Id, gift.Name, gift.Priority, gift.IsBought});
    }
}