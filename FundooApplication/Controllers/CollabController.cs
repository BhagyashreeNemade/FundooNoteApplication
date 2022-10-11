using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Context;
using System.Linq;
using System;
using RepositoryLayer.Service;
using RepositoryLayer.Interface;
using RepositoryLayer.Entity;
using System.Collections.Generic;

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
        [HttpDelete("Remove")]
        public IActionResult Remove(long collabid)
        {
            try
            {
                if (collabBL.Remove(collabid))
                {
                    return this.Ok(new { Success = true, message = "Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Unable to Delete" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("GetAllByNoteID")]
        public List<CollabEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return collabBL.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    
}
