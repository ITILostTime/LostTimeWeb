using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class EditPasswordViewModel
    {
        public string UserEmail { get; set; }
        public string UserOldPassword { get; set; }
        public string UserNewPassword { get; set; }
    }
}