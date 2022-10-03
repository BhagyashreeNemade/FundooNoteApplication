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
                return userInterfaceRL.UserRegitrations(userRegistration);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string Login(UserLogin userLogin)
        {
            try
            {
                return userInterfaceRL.Login(userLogin);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
