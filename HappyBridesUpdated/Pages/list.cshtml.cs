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
    private UserRepository _userRepository = new UserRepository();
    
    public string Message { get; set; }
    
    [BindProperty, Required]
    public string Name {get; set;}

    public List<Gift> Gifts = new List<Gift>();
    
    public bool IsBride { get; set; }
    public string KeyString { get; set; }
    
    
    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }

        CheckIsBride();
    }
    
    /// <summary>
    /// Adds a new gift to the database
    /// </summary>
    public void OnPostAdd()
    {
        CheckIsBride();
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

    public void OnGetBuy(int id)
    {
        string name = _userRepository.getBrideNameByUserId(int.Parse(HttpContext.Session.GetString("ID")));
        _giftRepository.BuyGift(id, 1, name);
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
    public void CheckIsBride()
    {
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            KeyString = HttpContext.Session.GetString("Key");
            IsBride = true;
        }
        else
        {
            IsBride = false;
        }
    }

    /// <summary>
    /// Reloads the list with gifts
    /// </summary>
    public void LoadGifts()
    {
        Gifts.Clear();
        if (IsBride && OwnsList())
        {
            Gifts = _giftRepository.GetOwnGifts(int.Parse(HttpContext.Session.GetString("ID")));
        }
        else
        {
            Gifts = _giftRepository.GetOtherGifts(HttpContext.Session.GetString("Key"));
        }
        Gifts = Gifts.OrderBy(g => g.Priority).ToList();
    }

    /// <summary>
    /// Checks if the key given for gifts list matches the key of the current login
    /// </summary>
    /// <returns>A boolean that is true if given key matches the key of the given id</returns>
    public bool OwnsList()
    {
        if (_giftRepository.OwnsList(HttpContext.Session.GetString("Key"), int.Parse(HttpContext.Session.GetString("ID"))))
        {
            return true;
        }
        return false;
    }

    public string getBrideName()
    {
        return _userRepository.getBrideNameByKey(HttpContext.Session.GetString("Key"));
    }
}