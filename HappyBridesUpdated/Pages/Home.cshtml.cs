using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class Home : PageModel
{
    public bool IsBride = false;
    
    public void OnGet()
    {
        if (HttpContext.Session.GetString("ID") == null)
        {
            Response.Redirect("/Index");
        }
        
        string test = HttpContext.Session.GetString("IsBride");
        if (HttpContext.Session.GetString("IsBride") == "true")
        {
            IsBride = true;
        }
    }

    public void OnPostGoToList()
    {
        Response.Redirect("/List");
    }

    public void OnPostStartList()
    {
        Response.Redirect("/List");
    }
}