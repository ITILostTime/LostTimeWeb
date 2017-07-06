using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class UserDeleteViewModel
    {
        public int UserID { get; set; }
        public string UserEmail { get; set; }
        public string UserConfirmPassword { get; set; }
    }
}