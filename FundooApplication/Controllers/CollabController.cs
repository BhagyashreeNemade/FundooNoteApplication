using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System.Linq;
using System;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        readonly ICollabBL collabBL;
       
        public CollabController(ICollabBL collabBL)
        {
            this.collabBL = collabBL;
            
        }
        [HttpPost("AddCollaborator")]
        public IActionResult AddCollab(long noteid, string email)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);


                var result = collabBL.AddCollab(noteid, userid, email);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Collaborator Added Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to add note" });
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
