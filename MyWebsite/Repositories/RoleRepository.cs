using MyWebsite.EF;
using MyWebsite.Models;

namespace MyWebsite.Repositories
{
    public class RoleRepository
    {
        // Read all
        public List<RoleVM> GetAll()
        {
            var list = new List<RoleVM>();
            using (var _context = new WebsiteContext())
            {
                list = (from x in _context.Roles
                        where x.IsActive == true
                        select new RoleVM
                        {
                            RoleId = x.RoleId,
                            RoleName = x.RoleName,
                            Description = x.Description
                        }).OrderBy(x => x.RoleId).ToList();
            }
            return list;
        }

        public bool SetUserRole(UserVM model)
        {
            bool isSave = false;
            using (var _context = new WebsiteContext())
            {
                var oUserRole = (from ur in _context.UserRoles where ur.UserId == model.UserID select ur).FirstOrDefault();
                if (oUserRole == null)
                {
                    oUserRole = new UserRole();
                    oUserRole.UserId = model.UserID;
                    oUserRole.RoleId = model.RoleId;
                    _context.UserRoles.Add(oUserRole);
                    _context.SaveChanges();
                }
                else
                {
                    oUserRole.RoleId = model.RoleId;
                    _context.SaveChanges();
                }
                isSave = true;
            }
            return isSave;
        }


    }
}
