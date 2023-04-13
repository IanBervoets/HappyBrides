using System.Security.Principal;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class list : PageModel
{
    public bool editing { get; set; }
    
    public void OnGet()
    {
        
    }

    public void OnPostEditing()
    {
        editing = true;
    }
}