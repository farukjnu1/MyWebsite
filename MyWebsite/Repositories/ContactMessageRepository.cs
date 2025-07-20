using MyWebsite.EF;
using MyWebsite.Models;

namespace MyWebsite.Repositories
{
    public class ContactMessageRepository
    {
        // Create
        public string Add(ContactMessageVM model)
        {
            string message = "operation failed.";
            using (var _context = new WebsiteContext())
            {
                ContactMessage oContactMessage = new ContactMessage();
                oContactMessage.CreateAt = DateTime.Now;
                oContactMessage.Email = model.Email;
                oContactMessage.FullName = model.FullName;
                oContactMessage.IsActive = true;
                oContactMessage.Message = model.Message;
                oContactMessage.Phone = model.Phone;
                oContactMessage.Subject = model.Subject;

                _context.ContactMessages.Add(oContactMessage);

                _context.SaveChanges();

                message = "message has been sent successfully.";
            }
            return message;
        }

        // Update
        public string Update(ContactMessageVM model)
        {
            string message = "operation failed.";
            using (var _context = new WebsiteContext())
            {
                ContactMessage? oContactMessage = (from x in _context.ContactMessages where x.ContactMessageId == model.ContactMessageId select x).FirstOrDefault();
                if (oContactMessage != null)
                {
                    oContactMessage.CreateAt = DateTime.Now;
                    oContactMessage.Email = model.Email;
                    oContactMessage.FullName = model.FullName;
                    oContactMessage.IsActive = model.IsActive;
                    oContactMessage.Message = model.Message;
                    oContactMessage.Phone = model.Phone;
                    oContactMessage.Subject = model.Subject;
                    oContactMessage.IsRead = model.IsRead;
                    oContactMessage.ReadBy = model.ReadBy;

                    _context.SaveChanges();

                    message = "data has been updated successfully.";
                }
            }
            return message;
        }

        // Delete
        public string Delete(int id)
        {
            string message = "operation failed.";
            using (var _context = new WebsiteContext())
            {
                ContactMessage? oContactMessage = (from x in _context.ContactMessages where x.ContactMessageId == id select x).FirstOrDefault();
                if (oContactMessage != null)
                {
                    _context.ContactMessages.Remove(oContactMessage);

                    _context.SaveChanges();

                    message = "data has been removed successfully.";
                }
            }
            return message;
        }

        // Read all
        public List<ContactMessageVM> GetAll()
        {
            var list = new List<ContactMessageVM>();

            using (var _context = new WebsiteContext())
            {
                list = (from x in _context.ContactMessages
                        select new ContactMessageVM
                        {
                            ContactMessageId = x.ContactMessageId,
                            CreateAt = x.CreateAt,
                            Email = x.Email,
                            FullName = x.FullName,
                            IsActive = x.IsActive,
                            Message = x.Message,
                            Phone = x.Phone,
                            Subject = x.Subject,
                            IsRead = x.IsRead,
                            ReadBy = x.ReadBy
                        }).ToList();
            }

            return list;
        }

        // Read one
        public ContactMessageVM? GetById(int id)
        {
            ContactMessageVM? model = null;

            using (var _context = new WebsiteContext())
            {
                model = (from x in _context.ContactMessages
                         where x.ContactMessageId == id
                         select new ContactMessageVM
                         {
                             ContactMessageId = x.ContactMessageId,
                             CreateAt = x.CreateAt,
                             Email = x.Email,
                             FullName = x.FullName,
                             IsActive = x.IsActive,
                             Message = x.Message,
                             Phone = x.Phone,
                             Subject = x.Subject,
                             IsRead = x.IsRead,
                             ReadBy = x.ReadBy
                         }).FirstOrDefault();
            }

            return model;
        }
    }
}
