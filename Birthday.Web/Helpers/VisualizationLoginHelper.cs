using Birthday.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Birthday.Web.Helpers
{
    public class VisualizationLoginHelper
    {
        public static bool ValidateUser(string email, string password, HttpSessionStateBase session)
        {
            using (var service = new UserService())
            {
                int userID = -1;
                int birthdayID = -1;

                if (service.Validate(email, password, ref userID, ref birthdayID))
                {
                    session["UserID"] = userID;
                    session["BirthdayID"] = birthdayID;

                    return true;
                }
            }

            return false;
        }
    }
}