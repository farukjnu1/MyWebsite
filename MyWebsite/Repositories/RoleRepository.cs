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
                        select new RoleVM
                        {
                            RoleId = x.RoleId,
                            RoleName = x.RoleName,
                            Description = x.Description
                        }).OrderBy(x => x.RoleId).ToList();
            }

            return list;
        }
    }
}
