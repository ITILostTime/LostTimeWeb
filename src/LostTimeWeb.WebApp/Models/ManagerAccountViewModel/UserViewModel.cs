using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string UserPseudonym { get; set; }
        public string UserEmail { get; set; }
        public byte[] UserPassword { get; set; }
        public DateTime UserAccountCreationDate { get; set; }
        public DateTime UserLastConnectionDate { get; set; }
        public string UserGoogleToken { get; set; }
        public string UserGoogleID { get; set; }
        public string UserPermission { get; set; }
    }
}