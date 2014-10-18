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

        public int ReserveBirthday(DateTime eventDate, string email, string firstName, string lastName, ref string password)
        {
            var now = DateTime.Now;

            var birthday = new Birthday
            {
                CreateDate = now,
                EventDate = eventDate,
                Published = false
            };

            password = CodeGenerator.GetCode(Config.PasswordLength);

            var user = new User
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName,
                CreateDate = now,
                ExpireDate = eventDate,
                Password = password
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

            if (birthday.TemplateID == templateID)
            {
                return false;
            }

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

            foreach (var item in birthday.BirthdayTexts.ToList())
            {
                _DbContext.Entry(item).State = System.Data.EntityState.Deleted;
                birthday.BirthdayTexts.Remove(item);
            }

            for (int i = 0; i < template.TextCount; i++)
            {
                birthday.BirthdayTexts.Add(new BirthdayText
                {
                    TextIndex = i
                });
            }

            Update(birthday);

            SaveChanges();

            return true;
        }

        public List<BirthdayImage> GetBirthdayImages(int birthdayID)
        {
            return _DbContext.BirthdayImages
                .Where(x => x.BirthdayID == birthdayID)
                .ToList();
        }

        public Birthday GetCurrentBirthday()
        {
            var today = DateTime.Today;

            var birthday = _DbContext.Birthdays.FirstOrDefault(x => x.EventDate == today && x.Published);

            return birthday;
        }

        public void UpdateBirthdayText(int birthdayID, int index, string text)
        {
            var birthday = this.Get(birthdayID);

            var birthdayText = birthday.BirthdayTexts
                .FirstOrDefault(x => x.TextIndex == index);

            if (birthdayText != null)
            {
                birthdayText.Text = text;

                this.Update(birthday);

                this.SaveChanges();
            }
        }
    }
}
