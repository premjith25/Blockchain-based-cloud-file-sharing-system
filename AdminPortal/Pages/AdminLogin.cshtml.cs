using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace AdminPortal.Pages;

public class AdminLoginModel : PageModel
{
    private readonly IConfiguration _config;

    public AdminLoginModel(IConfiguration config)
    {
        _config = config;
    }

    [BindProperty] public string Email { get; set; } = "";
    [BindProperty] public string Password { get; set; } = "";
    public string? Error { get; set; }

    public void OnGet() { }

    public IActionResult OnPost()
    {
        var hash = Convert.ToHexString(
            SHA256.HashData(Encoding.UTF8.GetBytes(Password ?? ""))).ToLowerInvariant();

        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT FullName FROM Users WHERE Email = @email AND PasswordHash = @hash AND Role = 'Admin'",
            conn);
        cmd.Parameters.AddWithValue("@email", Email ?? "");
        cmd.Parameters.AddWithValue("@hash", hash);

        var name = cmd.ExecuteScalar() as string;
        if (name != null)
        {
            HttpContext.Session.SetString("AdminName", name);
            return RedirectToPage("AdminHome");
        }

        Error = "Invalid email or password.";
        return Page();
    }
}
