using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class EditViewModel
    {
        public int UserID { get; set; }
        public string UserPseudonym { get; set; }
        public string UserEmail { get; set; }
        public string UserOldPassword { get; set; }
        public string UserNewPassword { get; set; }
        public DateTime UserAccountCreationDate { get; set; }
        public DateTime UserLastConnectionDate { get; set; }
        public string UserGoogleToken { get; set; }
        public int UserGoogleID { get; set; }
        public string UserPermission { get; set; }
    }
}