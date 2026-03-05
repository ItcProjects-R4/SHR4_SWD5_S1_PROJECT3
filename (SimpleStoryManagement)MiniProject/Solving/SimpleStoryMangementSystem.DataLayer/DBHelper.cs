using Microsoft.Data.SqlClient;

namespace SimpleStoryMangementSystem.DataLayer;

public class DBHelper
{
    // ===== غير الـ Connection String دي بـ Server و Database بتاعك =====
    private static readonly string _connectionString =
        "Server=.;Database=SimpleStoryManagementDB;Trusted_Connection=True;TrustServerCertificate=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(_connectionString);
    }
}
