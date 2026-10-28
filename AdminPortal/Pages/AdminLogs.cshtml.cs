using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AdminPortal.Pages;

public class LogRow
{
    public int LogId { get; set; }
    public string UserName { get; set; } = "";
    public string FileName { get; set; } = "";
    public string Action { get; set; } = "";
    public DateTime LoggedAt { get; set; }
}

public class AdminLogsModel : PageModel
{
    private readonly IConfiguration _config;

    public AdminLogsModel(IConfiguration config)
    {
        _config = config;
    }

    public List<LogRow> Logs { get; set; } = new();

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("AdminName") == null)
        {
            return RedirectToPage("AdminLogin");
        }

        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        conn.Open();
        using var cmd = new SqlCommand(
            @"SELECT l.LogId, u.FullName, f.FileName, l.Action, l.LoggedAt
              FROM ActivityLogs l
              JOIN Users u ON l.UserId = u.UserId
              JOIN Files f ON l.FileId = f.FileId
              ORDER BY l.LoggedAt DESC, l.LogId DESC", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Logs.Add(new LogRow
            {
                LogId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                FileName = reader.GetString(2),
                Action = reader.GetString(3),
                LoggedAt = reader.GetDateTime(4)
            });
        }
        return Page();
    }
}
