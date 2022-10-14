using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        private readonly IUserInterfaceRL userInterfaceRL;

        public UserBL(IUserInterfaceRL userInterfaceRL)
        {
            this.userInterfaceRL = userInterfaceRL;
        }

      

        public UserEntity UserRegitrations(UserRegistration userRegistration)
        {
            try
            {
                return this.userInterfaceRL.UserRegitrations(userRegistration);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return this.userInterfaceRL.Login(userLogin);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public string GenerateJWTToken(string EmailId, long UserId)
        {
            try
            {
                return userInterfaceRL.GenerateJWTToken(EmailId,UserId);
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string ForgetPassword(string Emailid)
        {
            try
            {
                return userInterfaceRL.ForgetPassword(Emailid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ResetPassword(string Emailid, string password, string confirmpassword)
        {
            try
            {
                return userInterfaceRL.ResetPassword(Emailid, password, confirmpassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
