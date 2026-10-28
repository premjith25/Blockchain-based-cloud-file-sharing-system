using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AdminPortal.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _config;
    public List<string> Users { get; set; } = new();
    public string? Error { get; set; }

    public IndexModel(IConfiguration config)
    {
        _config = config;
    }

    public void OnGet()
    {
        try
        {
            using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            conn.Open();
            using var cmd = new SqlCommand("SELECT Email, Role FROM Users", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Users.Add($"{reader.GetString(0)} ({reader.GetString(1)})");
            }
        }
        catch (Exception ex)
        {
            Error = ex.Message;
        }
    }
}
