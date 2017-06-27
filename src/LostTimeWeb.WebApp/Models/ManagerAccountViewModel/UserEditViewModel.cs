using System;

namespace LostTimeWeb.WebApp.Models.ManagerAccountViewModel
{
    public class UserEditViewModel
    {
        public int UserID { get; set; }
        public string UserPseudonym { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string UserControlPassword { get; set; }
        public string UserGoogleID { get; set; }
    }
}