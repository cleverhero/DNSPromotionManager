using System;
using Microsoft.AspNetCore.Http;

namespace DNSPromotionManager.Models
{
    public class UserManager
    {
        private static UserManager instance;
        private HttpContext context;

        private UserManager()
        {
            context = new HttpContextAccessor().HttpContext;
        }
        
        private static UserManager getInstance()
        {
            if (instance == null)
                instance = new UserManager();
            return instance;
        }

        private User PGetUser()
        {
            if (context.User.Identity.IsAuthenticated)
                return GetAuthenticatedUser();

            if (context.Request.Cookies["Role"] == null)
                CreateGuest();

            return GetGuest();
        }

        private User GetGuest()
        {
            return new User()
            {
                Name = "Guest",
                Branch = new Branch()
                {
                    Name = context.Request.Cookies["Branch"]
                },
                Role = new Role()
                {
                    Name = "Guest"
                }
            };
        }

        private void CreateGuest()
        {
            CookieOptions cookie = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1)
            };

            context.Response.Cookies.Append("Branch", "Владивосток", cookie);


        }

        private User GetAuthenticatedUser()
        {
            return null;
        }

        public static User GetUser()
        {
            return UserManager.getInstance().PGetUser();
        }
    }
}
