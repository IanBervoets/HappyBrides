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
            string test = Name;
            Gift gift = new Gift();
            gift.Name = test;
            Gifts.Add(gift);
            //_giftRepository.AddGift(gift);
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