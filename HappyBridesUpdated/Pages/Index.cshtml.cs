using HappyBridesUpdated.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HappyBridesUpdated.Pages;

public class IndexModel : PageModel
{
    public static ConnectRepository Repository = new();
    
    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        
    }
    

    

}