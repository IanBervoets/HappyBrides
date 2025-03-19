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
    
    [BindProperty, Required]
    public int currentGift {get; set;}
    
    public bool IsBride { get; set; }

    public bool test;
    
    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }

        IsBride = CheckIsBride();
    }
    
    public void OnPostAdd()
    {
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
                gift.Priority = 1;
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

    public void OnGetRemove(int id)
    {
        _giftRepository.RemoveGift(id);
        LoadGifts();
    }

    public void OnGetEdit()
    {
        
    }

    public bool CheckIsBride()
    {
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            return true;
        }
        return false;
    }

    public void LoadGifts()
    {
        Gifts.Clear();
        Gifts = _giftRepository.GetGifts(int.Parse(HttpContext.Session.GetString("ID")));
    }
}