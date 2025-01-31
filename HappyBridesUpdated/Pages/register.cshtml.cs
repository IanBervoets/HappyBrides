using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HappyBridesUpdated.Pages;

public class register : PageModel
{
    public RegisterRepository Repository = new RegisterRepository();
    
    [BindProperty, Required(ErrorMessage = "Please enter your names")]
    public string Name { get; set; }
    
    [BindProperty, Required(ErrorMessage = "Please make sure to enter a valid e-mailadress."), 
     EmailAddress(ErrorMessage = "Please make sure to enter a valid e-mailadress.")] 
    public string Email { get; set; }
    
    [BindProperty]
    public int IsBride { get; set; }
    
    [BindProperty, Required(ErrorMessage = "Please make sure to enter a password between 8 and 16 characters that contains a capital letter and a Number."), RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,16}$", ErrorMessage = "Please make sure to enter a password between 8 and 16 characters that contains a capital letter and a Number.")]
    public string PassWD { get; set; }
    
    [BindProperty, Required(ErrorMessage = "Passwords don't match."),RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,16}$", ErrorMessage = "Please make sure to enter a password between 8 and 16 characters that contains a capital letter and a Number.")]
    public string PassConfirm { get; set; }
    
    public string Message { get; set; }

    
    private static Random random = new Random();

    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string CreateKey()
    {
        string keyString = RandomString(8);
        
        if (RegisterRepository.CheckKey("keyString"))
        {
            CreateKey();
        }

        return keyString;
    }
    
    public void OnGet()
    {
        
    }

    public void OnPost()
    {
        bool isRegistered = RegisterRepository.EmailNotRegistered(Email);
        bool passwordMatches = PassWD == PassConfirm;
        
        if (ModelState.IsValid && passwordMatches && !isRegistered)
        {
            int user = RegisterRepository.RegisterAccount(Name, Email, IsBride, PassWD, CreateKey());
            HttpContext.Session.SetString("ID", JsonConvert.SerializeObject(user));
            Response.Redirect("List");
        }
        else
        {
            if (isRegistered)
            {
                Message = "E-mailadress is already in use.";
            }
            else if(!passwordMatches)
            {
                Message = "Passwords don't match";
            }
            else
            {
                Message = "Please fill in the fields correctly.";
            }
        }
    }
}