using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AdminPortal.Pages;

public class UserRow
{
    public int UserId { get; set; }
    public string FullName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}

public class AdminUsersModel : PageModel
{
    private readonly IConfiguration _config;

    public AdminUsersModel(IConfiguration config)
    {
        _config = config;
    }

    public List<UserRow> Users { get; set; } = new();

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("AdminName") == null)
        {
            return RedirectToPage("AdminLogin");
        }

        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        conn.Open();
        using var cmd = new SqlCommand(
            "SELECT UserId, FullName, Email, Role, CreatedAt FROM Users ORDER BY UserId", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Users.Add(new UserRow
            {
                UserId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                Role = reader.GetString(3),
                CreatedAt = reader.GetDateTime(4)
            });
        }
        return Page();
    }
}
