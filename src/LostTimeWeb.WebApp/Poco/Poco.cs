using LostTimeDB;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
namespace LostTimeWeb.WebApp.Poco
{
    
     public class ModelPoco
    {
        private int _userId = 001;
        private string _email = "vanbutsele@intechinfo.fr";
        private string _password = "QWERTY012345";
        private string _role = "Admin";
        
        public UserAccount checkModel(string email, string password )
        {
            PasswordHasher _passwordHasher = new PasswordHasher();
            UserAccount user = new UserAccount();
            user.UserID = _userId;
            user.UserPassword = _passwordHasher.HashPassword( _password );
            user.UserEmail = _email;
            user.Role = _role;
            if( user.Email == email && _passwordHasher.VerifyHashedPassword( user.Password, password ) == PasswordVerificationResult.Success )
            {
                return user;
            }
            return null;
        }
    }
}