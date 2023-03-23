﻿using ESB.application.DTOs.Security;
using Microsoft.AspNetCore.Http;
using System;

namespace ESB.Infrastructure.Services
{
    public class BaseService
    {
        private readonly IHttpContextAccessor _context;

        public BaseService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public ApplicationUser GetLoggerUser()
        {
            var User = _context.HttpContext.User;

            if (User.Identity.IsAuthenticated)
            {

                var UserData = new ApplicationUser()
                {
                    Id = User.FindFirst("uid").Value,
                    Name = User.FindFirst("name").Value,
                    Email = User.FindFirst("email").Value,
                };

                return UserData;
            }
            return null;
        }


        public int GetLoggerUserId()
        {
            var User = _context.HttpContext.User;
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    return Convert.ToInt32(User.FindFirst("uid").Value);
                }
                catch (Exception)
                {

                }

            }
            return 0;

        }



    }
}
