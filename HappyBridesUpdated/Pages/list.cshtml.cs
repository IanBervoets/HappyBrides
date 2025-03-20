using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using HappyBridesUpdated.Model;
using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class GiftsList : PageModel
{
    private GiftRepository _giftRepository = new GiftRepository();
    
    public string Message { get; set; }
    
    [BindProperty, Required]
    public string Name {get; set;}

    public List<Gift> Gifts = new List<Gift>();
    
    public bool IsBride { get; set; }
    public string KeyString { get; set; }

    public bool test;
    
    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }

        IsBride = CheckIsBride();
    }
    
    /// <summary>
    /// Adds a new gift to the database
    /// </summary>
    public void OnPostAdd()
    {
        LoadGifts();
        if (ModelState.IsValid)
        {
            Gift gift = new Gift();
            gift.Name = Name;
            if (Gifts.Any())
            {
                gift.Priority = Gifts.Max(g => g.Priority) + 1;
            }
            else
            {
                gift.Priority = 0;
            }
            gift.idUsers = int.Parse(HttpContext.Session.GetString("ID"));
            _giftRepository.AddGift(gift);
            Gifts.Add(gift);
        }
        else
        {
            Message = "Add a name";
        }
    }

    /// <summary>
    /// Removes a gift from the database
    /// </summary>
    /// <param name="id">The id of the gift that has to be removed</param>
    public void OnGetRemove(int id)
    {
        _giftRepository.RemoveGift(id);
        LoadGifts();
    }

    /// <summary>
    /// Changes the priority order of the gifts
    /// </summary>
    /// <param name="priority">The priority of the gift</param>
    /// <param name="direction">Whether the priority of the gift should be lowered or increased</param>
    public void OnGetEdit(int priority, string direction)
    {
        LoadGifts();
        if (direction == "down")
        {
            Gift downGift = Gifts[priority];
            Gift upGift = Gifts[priority + 1];
            downGift.Priority++;
            upGift.Priority--;
            _giftRepository.UpdateGift(downGift);
            _giftRepository.UpdateGift(upGift);
            LoadGifts();
        }

        if (direction == "up")
        {
            Gift upGift = Gifts[priority];
            Gift downGift = Gifts[priority - 1];
            upGift.Priority--;
            downGift.Priority++;
            _giftRepository.UpdateGift(upGift);
            _giftRepository.UpdateGift(downGift);
            LoadGifts();
        }
    }

    /// <summary>
    /// Checks if the user is hosting a wedding
    /// </summary>
    /// <returns>A boolean that is true if the user is hosting a wedding</returns>
    public bool CheckIsBride()
    {
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            KeyString = HttpContext.Session.GetString("KeyString").TrimEnd('"').TrimStart('"');
            return true;
        }
        return false;
    }

    /// <summary>
    /// Reloads the list with gifts
    /// </summary>
    public void LoadGifts()
    {
        Gifts.Clear();
        Gifts = _giftRepository.GetGifts(int.Parse(HttpContext.Session.GetString("ID")));
        Gifts = Gifts.OrderBy(g => g.Priority).ToList();
    }
}