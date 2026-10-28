using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AdminPortal.Pages;

public class TamperedRow
{
    public int FileId { get; set; }
    public string FileName { get; set; } = "";
    public string OwnerName { get; set; } = "";
    public DateTime UploadedAt { get; set; }
}

public class AdminTamperedModel : PageModel
{
    private readonly IConfiguration _config;

    public AdminTamperedModel(IConfiguration config)
    {
        _config = config;
    }

    public List<TamperedRow> Files { get; set; } = new();

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("AdminName") == null)
        {
            return RedirectToPage("AdminLogin");
        }

        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        conn.Open();
        using var cmd = new SqlCommand(
            @"SELECT f.FileId, f.FileName, u.FullName, f.UploadedAt
              FROM Files f
              JOIN Users u ON f.OwnerId = u.UserId
              WHERE f.IsTampered = 1
              ORDER BY f.UploadedAt DESC", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Files.Add(new TamperedRow
            {
                FileId = reader.GetInt32(0),
                FileName = reader.GetString(1),
                OwnerName = reader.GetString(2),
                UploadedAt = reader.GetDateTime(3)
            });
        }
        return Page();
    }
}
