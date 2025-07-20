using Microsoft.Data.SqlClient;
using MyWebsite.Models;
using System.Data;
using MyWebsite.EF;

namespace MyWebsite.Repositories
{
    public class MediaRepository
    {
        private readonly string _connectionString = "Server=Faruk-Abdullah;Database=Website;User=sa;Password=123;Trusted_Connection=True;TrustServerCertificate=True;";

        // Read
        public List<SiteSettingVM> GetAll(UserVM user)
        {
            List<SiteSettingVM> list = new List<SiteSettingVM>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SiteSetting_Read", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@QueryType", QueryType.GetAll);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SiteSettingVM model = new SiteSettingVM();
                            model.SettingValue = reader.GetString("SettingValue");
                            model.SettingKey = reader.GetString("SettingKey");

                            list.Add(model);
                        }
                    }
                    conn.Close();
                }
            }
            return list;
        }

        // Read by id
        public List<SiteSettingVM> GetById(string SettingKey)
        {
            List<SiteSettingVM> list = new List<SiteSettingVM>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SiteSetting_Read", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@SettingKey", SettingKey);
                    cmd.Parameters.AddWithValue("@QueryType", QueryType.GetById);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SiteSettingVM model = new SiteSettingVM();
                            model.SettingValue = reader.GetString("SettingValue");
                            model.SettingKey = reader.GetString("SettingKey");

                            list.Add(model);
                        }
                    }
                    conn.Close();
                }
            }
            return list;
        }

        public enum QueryType
        {
            GetAll = 0, GetById = 1, Insert = 2, Update = 3, Delete = 4
        }

    }
}
