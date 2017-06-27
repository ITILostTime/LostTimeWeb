using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class EditPasswordViewModel
    {
        public int UserID { get; set; }
        public string UserOldPassword { get; set; }
        public string UserNewPassword { get; set; }
    }
}