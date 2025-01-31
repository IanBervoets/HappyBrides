using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class Home : PageModel
{
    public string test;
    
    public void OnGet()
    { 
        test = HttpContext.Session.GetString("isBride");
    }
}