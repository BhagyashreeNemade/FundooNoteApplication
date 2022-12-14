using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
        public UserEntity UserRegitrations(UserRegistration userRegistration);
        public string Login(UserLogin userLogin);
        public string GenerateJWTToken(string Emailid, long UserId);
        public string ForgetPassword(string Emailid);
        public bool ResetPassword(string email, string password, string confirmpassword);
    }
}
