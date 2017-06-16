using LostTimeWeb.DAL;
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
namespace LostTimeWeb.WebApp.Poco
{
    
    static public class Poco
    {
        private int _userId = 001;
        private string _email = "vanbutsele@intechinfo.fr";
        private string _password = "QWERTY012345";
        private string _role = "Admin"
        
        public User checkModel(string email, string password )
        {
            User user = new User();
            user.UserId = _userId;
            user.Password = _passwordHasher.HashPassword( _password );
            user.Email = _email;
            user.Role = _role;
            if( user._email == email && _passwordHasher.VerifyHashedPassword( user._password, password ) == PasswordVerificationResult.Success )
            {
                return user;
            }
            return null;
        }
    }
}