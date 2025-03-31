using System.ComponentModel.DataAnnotations;
using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class Home : PageModel
{
    private UserRepository _userRepository = new UserRepository();
    
    public bool IsBride = false;

    [BindProperty, Required] 
    public string Key { get; set; }

    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }
        
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            IsBride = true;
        }
    }

    public void OnPostGoToOwnList()
    {
        Response.Redirect("/List");
    }

    public void OnPostGoToOtherList()
    {
        HttpContext.Session.SetString("Key", Key);
        Response.Redirect("/List");
    }

    public void OnPostBecomeBride()
    {
        HttpContext.Session.SetString("IsBride", "true");
        _userRepository.ChangeBrideStatus(int.Parse(HttpContext.Session.GetString("ID")), 1);
        Response.Redirect("/List");
    }
}