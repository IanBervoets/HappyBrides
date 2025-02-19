using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class list : PageModel
{
    public bool editing { get; set; }
    
    public string Message { get; set; }
    
    [BindProperty, Required]
    public string Name {get; set;}
    
    public void OnGet()
    {
        
    }

    public void OnPostEditing()
    {
        editing = true;
    }
    
    
    /*public void OnPostAdd()
    {
        string test = Name;
        if (String.IsNullOrEmpty(test))
        {
            editing = true;
            Message = "Add a name";
        }
        else
        {
            editing = true;

        }
    }*/
}