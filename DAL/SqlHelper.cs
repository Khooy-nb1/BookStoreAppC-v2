using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BookStoreApp
{
    public static class SqlHelper
    {
        public static string ConnStr =
            ConfigurationManager.ConnectionStrings["BookStoreDB"].ConnectionString;

        public static DataTable Query(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
            }
        }

        public static int Execute(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteNonQuery();
            }
        }

        public static object Scalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection conn = new SqlConnection(ConnStr))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                if (ps != null && ps.Length > 0) cmd.Parameters.AddRange(ps);
                return cmd.ExecuteScalar();
            }
        }
    }
}
