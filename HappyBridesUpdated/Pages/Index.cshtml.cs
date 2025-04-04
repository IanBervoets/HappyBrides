﻿using System.ComponentModel.DataAnnotations;
using HappyBridesUpdated.Model;
using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HappyBridesUpdated.Pages;

public class IndexModel : PageModel
{
    private static LoginRepository _loginRepository = new LoginRepository();
        
    [BindProperty, Required(ErrorMessage = "Please enter a valid email address.")]
    public string Email { get; set; }
    
    [BindProperty, Required(ErrorMessage = "Please enter a valid password.")]
    public string Password { get; set; }
    
    public string Msg { get; set; }

    public void OnGet()
    {
        
    }

    /// <summary>
    /// Creates a session with a userid and whether the user is hosting a wedding or attending one
    /// </summary>
    public void OnPost()
    {
        if (ModelState.IsValid && _loginRepository.CheckAccount(Email, Password))
        {
            User user = _loginRepository.GetUser(Email, Password);
            
            HttpContext.Session.SetString("ID", JsonConvert.SerializeObject(user.Id));
            if (user.IsBride)
            {
                HttpContext.Session.SetString("IsBride", "true");
                HttpContext.Session.SetString("Key", JsonConvert.SerializeObject(user.KeyString).TrimEnd('"').TrimStart('"'));
            }
            else
            {
                HttpContext.Session.SetString("isBride", "false");
            }
            Response.Redirect("/Home");
        }
        Msg = "Invalid email or password.";
    }
}