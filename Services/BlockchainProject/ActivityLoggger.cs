public class ActivityLogger
{
    private string connectionString = "Server=your_server;Database=CloudFileShareDB;Trusted_Connection=True;";

    public void LogActivity(int userId, string actionType, string fileName)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO ActivityLogs (UserID, ActionType, FileName, Timestamp) VALUES (@UserID, @ActionType, @FileName, GETDATE())";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@ActionType", actionType); // e.g., "DOWNLOAD" or "VIEW"
                cmd.Parameters.AddWithValue("@FileName", fileName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}