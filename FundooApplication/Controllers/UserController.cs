using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

            private readonly IUserBL userBL;
        private readonly ILogger<UserController> logger;


        public UserController(IUserBL userBL, ILogger<UserController> logger)
            {
                this.userBL = userBL;
            this.logger = logger;
        }
        
       
            [HttpPost("Register")]
            public IActionResult Register(UserRegistration userRegistration)
            {
                try
                {
                    var result = userBL.UserRegitrations(userRegistration);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = "User Registration Succesfull", data = result });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = "User Registration UnSuccesfull" });
                    }
                }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
       // [Authorize]
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
           
            try
            {
                var result = userBL.Login(userLogin);
               
                if (result != null)
                {
                    logger.LogInformation("You have logged in sucessfully");
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Login Successfull",
                        token = result
                    }
                        );
                }
                else
                {
                    logger.LogInformation("You have logged in unsucessfull");
                    return BadRequest(new { Success = false, message = "login Unsucessfull" });
                }
                

            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex.InnerException;
            }
        }
        [HttpPost("ForgetPassword")]
        public IActionResult ForgetPassword(string email)
        {

            try
            {
                string token = userBL.ForgetPassword(email);
                if (token != null)
                {
                    return Ok(new { success = true, Message = "Please check your Email.Token sent succesfully." });
                }
                else
                {
                    return this.BadRequest(new { Success = false, Message = "Email not registered" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var user = userBL.ResetPassword(email, password, confirmpassword);
                if (!user)
                {
                    return this.BadRequest(new { success = false, message = "enter valid password" });

                }



                else
                {
                    return this.Ok(new { success = true, message = "reset password is successful" });
                }




            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

