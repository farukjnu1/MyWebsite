using System;
using System.Data;
using Microsoft.Data.SqlClient;
using MyWebsite.EF;
using MyWebsite.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MyWebsite.Repositories
{
    public class SpecialistRepository
    {
        // Read all
        public List<SpecialistVM> GetAll()
        {
            var list = new List<SpecialistVM>();

            using (var _context = new WebsiteContext())
            {
                list = (from x in _context.Specialists
                        select new SpecialistVM
                        {
                            Description = x.Description,
                            Designation = x.Designation,
                            FullName = x.FullName,
                            IsActive = x.IsActive,
                            SpecialistId = x.SpecialistId   
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
