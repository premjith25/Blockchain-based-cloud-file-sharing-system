using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminPortal.Pages;

public class AdminHomeModel : PageModel
{
    public string AdminName { get; set; } = "";

    public IActionResult OnGet()
    {
        var name = HttpContext.Session.GetString("AdminName");
        if (name == null)
        {
            return RedirectToPage("AdminLogin");
        }
        AdminName = name;
        return Page();
    }

    public IActionResult OnPostLogout()
    {
        HttpContext.Session.Clear();
        return RedirectToPage("AdminLogin");
    }
}
