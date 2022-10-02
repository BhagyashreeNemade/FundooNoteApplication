using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FundooApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
            private readonly IUserBL userBL;
            public UserController(IUserBL userBL)
            {
                this.userBL = userBL;
            }

            //[Route("Register")]
            [HttpPost("Register")]
            public IActionResult Regispostration(UserRegistration userRegistration)
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
                catch (Exception)
                {
                    throw;
                }
            }
        }
}

