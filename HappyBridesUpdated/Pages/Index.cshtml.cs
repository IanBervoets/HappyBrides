using System.ComponentModel.DataAnnotations;
using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class IndexModel : PageModel
{
    private static LoginRepository _loginRepository = new LoginRepository();
        
    [BindProperty, Required(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }
    
    [BindProperty, Required(ErrorMessage = "Please enter a valid password.")]
    public string Password { get; set; }
    
    public string msg { get; set; }
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        if (ModelState.IsValid && _loginRepository.Login(Email, Password))
        {
            Response.Redirect("/Home");
        }
        msg = "Invalid email or password.";
    }
    

    

}