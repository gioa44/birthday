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

        public Template GetTemplate(int templateID)
        {
            return new TemplateService(_DbContext).Get(templateID);
        }
    }
}
