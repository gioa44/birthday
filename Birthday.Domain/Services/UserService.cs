using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Domain.Services
{
    public class UserService : DomainServiceBase<User>
    {
        public UserService() { }

        public UserService(BirthdayContext context)
            : base(context) { }

        public UserService(BirthdayContextContainer container)
            : base(container) { }

        public bool Validate(string email, string password, ref int userID, ref int birthdayID)
        {
            var user = _DbContext.Users.FirstOrDefault(x => x.Email == email && x.Password == password);

            if (user != null)
            {
                userID = user.UserID;
                birthdayID = user.Birthdays
                    .Select(x => x.BirthdayID)
                    .First();

                return true;
            }

            return false;
        }
    }
}
