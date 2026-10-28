using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace AdminPortal.Pages;

public class FeedbackRow
{
    public int FeedbackId { get; set; }
    public string UserName { get; set; } = "";
    public string Message { get; set; } = "";
    public DateTime SubmittedAt { get; set; }
}

public class AdminFeedbackModel : PageModel
{
    private readonly IConfiguration _config;

    public AdminFeedbackModel(IConfiguration config)
    {
        _config = config;
    }

    public List<FeedbackRow> Items { get; set; } = new();

    public IActionResult OnGet()
    {
        if (HttpContext.Session.GetString("AdminName") == null)
        {
            return RedirectToPage("AdminLogin");
        }

        using var conn = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        conn.Open();
        using var cmd = new SqlCommand(
            @"SELECT fb.FeedbackId, u.FullName, fb.Message, fb.SubmittedAt
              FROM Feedback fb
              JOIN Users u ON fb.UserId = u.UserId
              ORDER BY fb.SubmittedAt DESC, fb.FeedbackId DESC", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Items.Add(new FeedbackRow
            {
                FeedbackId = reader.GetInt32(0),
                UserName = reader.GetString(1),
                Message = reader.GetString(2),
                SubmittedAt = reader.GetDateTime(3)
            });
        }
        return Page();
    }
}
