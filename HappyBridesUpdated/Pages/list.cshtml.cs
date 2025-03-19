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
    
    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }

        IsBride = CheckIsBride();
        //string test = HttpContext.Session.GetString("IsBride");

        
        Gifts = _giftRepository.GetGifts();
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
            gift.UserId = int.Parse(HttpContext.Session.GetString("ID"));
            _giftRepository.AddGift(gift);
            Gifts.Add(gift);
        }
        else
        {
            Message = "Add a name";
        }
    }

    public bool CheckIsBride()
    {
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            return true;
        }
        return false;
    }
}