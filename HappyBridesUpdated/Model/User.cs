namespace HappyBridesUpdated.Model;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Name { get; set; }
    public bool IsBride { get; set; }
    public string KeyString { get; set; }
}