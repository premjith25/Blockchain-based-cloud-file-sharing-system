using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;

public class AuthService
{
    private string connectionString = "Server=your_server;Database=CloudFileShareDB;Trusted_Connection=True;";

    // Password Hashing Function
    public string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    // User Registration
    public bool RegisterUser(string username, string email, string password)
    {
        string passwordHash = HashPassword(password);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "INSERT INTO Users (Username, Email, PasswordHash, CreatedAt) VALUES (@Username, @Email, @PasswordHash, GETDATE())";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                return rows > 0;
            }
        }
    }

    // User Login Verification
    public bool ValidateLogin(string username, string password, out int userId)
    {
        userId = -1;
        string passwordHash = HashPassword(password);
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            string query = "SELECT UserID FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@PasswordHash", passwordHash);
                
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    userId = Convert.ToInt32(result);
                    return true;
                }
            }
        }
        return false;
    }
}