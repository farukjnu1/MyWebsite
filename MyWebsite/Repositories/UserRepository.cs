using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyWebsite.EF;
using MyWebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyWebsite.Repositories
{
    public class UserRepository
    {
        private readonly string _connectionString = "Server=Faruk-Abdullah;Database=Website;User=sa;Password=123;Trusted_Connection=True;TrustServerCertificate=True;";

        // Create
        public void Add(UserVM user)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("User_Modify", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
            cmd.Parameters.AddWithValue("@QueryType", QueryType.Insert);

            conn.Open();
            //cmd.ExecuteNonQuery();
            string messageSQL = Convert.ToString(cmd.ExecuteScalar());
        }

        // Update
        public void Update(UserVM user)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("User_Modify", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Email", user.Email);
            cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            cmd.Parameters.AddWithValue("@Role", user.Role ?? "viewer");
            cmd.Parameters.AddWithValue("@UserID", user.UserID);
            cmd.Parameters.AddWithValue("@QueryType", QueryType.Update);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // Delete
        public void Delete(int userId)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("DELETE FROM Users WHERE UserID = @UserID", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@UserID", userId);
            cmd.Parameters.AddWithValue("@QueryType", QueryType.Delete);

            conn.Open();
            cmd.ExecuteNonQuery();
        }

        // Read all
        public List<UserVM> GetAll()
        {
            var users = new List<UserVM>();

            using (var _context = new WebsiteContext())
            {
                users = (from x in _context.Users
                         select new UserVM
                         {
                             CreatedAt = x.CreatedAt,
                             Email = x.Email,
                             PasswordHash = x.PasswordHash,
                             UserID = x.UserId,
                             Username = x.Username
                         }).ToList();
            }

            return users;
        }

        // Read one
        public UserVM GetById(int userId)
        {
            var user = new UserVM();

            using (var _context = new WebsiteContext())
            {
                user = (from x in _context.Users
                        where x.UserId == userId
                        select new UserVM
                        {
                            CreatedAt = x.CreatedAt,
                            Email = x.Email,
                            PasswordHash = x.PasswordHash,
                            UserID = x.UserId,
                            Username = x.Username
                        }).FirstOrDefault();
            }

            return user;
        }

        public enum QueryType
        {
            GetAll = 0, GetById = 1, Insert = 2, Update = 3, Delete = 4
        }

    }

}
