﻿namespace ESB.Application.DTOs.Security
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string IdentificationNumber { get; set; }
    }
}
