using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserInterfaceRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration config;
       
        public UserRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }

        public UserEntity UserRegitrations(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.EmailId = userRegistration.EmailId;
                userEntity.Password = userRegistration.Password;
                fundooContext.Add(userEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return userEntity;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception)
            {
                throw;
            }
         }
        public string Login(UserLogin userLogin)
        {
            try
            {
                var loginData = this.fundooContext.FundooDbTable.FirstOrDefault(x => x.EmailId == userLogin.EmailId && x.Password == userLogin.Password);
                if (loginData != null)
                {
                    var token = GenerateJWTToken(userLogin.EmailId, loginData.UserId);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public string GenerateJWTToken(string Emailid, long UserId)
        {
            try
            {
                var loginTokenHandler = new JwtSecurityTokenHandler();
                var loginTokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(this.config[("Jwt:key")]));
                var loginTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Email, Emailid),
                        new Claim("UserId", UserId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(15),
                    SigningCredentials = new SigningCredentials(loginTokenKey, SecurityAlgorithms.HmacSha256Signature)
                };
                var token = loginTokenHandler.CreateToken(loginTokenDescriptor);
                return loginTokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }
        }
        public string ForgetPassword(string Emailid)
        {
            try
            {
                var result = fundooContext.FundooDbTable.FirstOrDefault(x => x.EmailId == Emailid);
                if (result != null)
                {
                    var token = this.GenerateJWTToken(result.EmailId, result.UserId);
                    MSMQModel mSMQModel = new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string password, string confirmpassword)
        {
            try
            {
                if (password.Equals(confirmpassword))
                {
                    var user = fundooContext.FundooDbTable.Where(x => x.EmailId == email).FirstOrDefault();
                    user.Password = confirmpassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
