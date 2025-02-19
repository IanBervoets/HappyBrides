namespace HappyBridesUpdated.Model;

public class Gift
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; }
    public int Priority { get; set; }
    public bool IsBought { get; set; }
    public string BoughtBy { get; set; }
}