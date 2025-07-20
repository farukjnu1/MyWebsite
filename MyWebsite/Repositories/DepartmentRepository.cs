using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyWebsite.EF;
using MyWebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyWebsite.Repositories
{
    public class DepartmentRepository
    {
        // Read all
        public List<DepartmentVM> GetAll()
        {
            var list = new List<DepartmentVM>();

            using (var _context = new WebsiteContext())
            {
                list = (from x in _context.Departments
                        select new DepartmentVM
                        {
                            Code = x.Code,
                            DepartmentId = x.DepartmentId,
                            Description = x.Description,
                            IsActive = x.IsActive,
                            Name = x.Name,
                        }).ToList();
            }
            return list;
        }

        public enum QueryType
        {
            GetAll = 0, GetById = 1, Insert = 2, Update = 3, Delete = 4
        }

    }

}
