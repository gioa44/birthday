using Birthday.Properties;
using Birthday.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{
    public class BirthdayService : DomainServiceBase<Birthday>
    {
        public BirthdayService() { }

        public BirthdayService(BirthdayContext context)
            : base(context) { }

        public int ReserveBirthday(DateTime eventDate, string email)
        {
            var now = DateTime.Now;

            var birthday = new Birthday
            {
                CreateDate = now,
                EventDate = eventDate,
                Published = false
            };

            var pwd = CodeGenerator.GetCode(Config.PasswordLength);

            var user = new User
            {
                Email = email,
                CreateDate = now,
                ExpireDate = eventDate,
                Password = pwd
            };

            birthday.User = user;

            Add(birthday);

            SaveChanges();

            return birthday.BirthdayID;
        }

        public Birthday GetBirthday(int birthdayID)
        {
            return Get(birthdayID);
        }

        public Template GetTemplate(int birthdayID)
        {
            return Get(birthdayID).Template;
        }

        public int GetTemplateID(int birthdayID)
        {
            var templateID = _DbContext.Birthdays
                .Where(x => x.BirthdayID == birthdayID)
                .Select(x => x.TemplateID)
                .FirstOrDefault();

            if (templateID.HasValue)
            {
                return templateID.Value;
            }

            throw new Exception("Birthday has no template");
        }

        public bool SetTemplate(int birthdayID, int templateID)
        {
            var template = new TemplateService(_DbContext).Get(templateID);

            var birthday = Get(birthdayID);

            birthday.TemplateID = templateID;
            birthday.Html = template.Html;

            foreach (var item in birthday.BirthdayImages.ToList())
            {
                if (item.File != null)
                {
                    _DbContext.Files.Remove(item.File);
                }
                _DbContext.Entry(item).State = System.Data.EntityState.Deleted;
                birthday.BirthdayImages.Remove(item);
            }

            foreach (var item in template.TemplateImages)
            {
                birthday.BirthdayImages.Add(new BirthdayImage
                {
                    ImageIndex = item.ImageIndex,
                    ImageTop = item.ImageTop,
                    ImageLeft = item.ImageLeft,
                    ImageWidth = item.ImageWidth
                });
            }

            Update(birthday);

            SaveChanges();

            return true;
        }

        public List<BirthdayImage> GetBirthdayImages(int birthdayID)
        {
            return GetAll()
                .FirstOrDefault(x => x.BirthdayID == birthdayID)
                .BirthdayImages
                .ToList();
        }
    }
}
