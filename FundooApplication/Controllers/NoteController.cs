using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Linq;
using System;
using RepositoryLayer.Context;
using BusinessLayer.Service;
using Microsoft.AspNetCore.Http;

namespace FundooApplication.Controllers
{
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class NoteController : ControllerBase
        {
            INoteBL noteBL;
            FundooContext context;
            public NoteController(INoteBL noteBL, FundooContext context)
            {
                this.noteBL = noteBL;
                this.context = context;
            }
        [HttpGet("GetNotes")]
        public IActionResult GetNotes()
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var NotesList = noteBL.GetAllNotes(userid);

                return this.Ok(new { Success = true, message = "Get User Notes Successfully.", Data = NotesList });
            }
            catch (Exception)
            {
                throw;
            }
        }
        //[Authorize]
        [HttpPost("AddNote")]
            public IActionResult AddNotes(NoteModel addnote)
            {
                try
                {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                //long userid = User.FindFirst(ClaimTypes.UserId).Value;

                var result = noteBL.AddNote(addnote, userid);
                    if (result != null)
                    {
                        return this.Ok(new { Success = true, message = "Note Added Successfully", result = result });
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
        [HttpPut("UpdateNotes")]
        public IActionResult UpdateNotes(long noteid, NoteModel node)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.UpdateNotes(noteid, userId, node);

                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Notes Updated SuccessFully", noteid });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes updation failed" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message, stackTrace = e.StackTrace });
            }

        }

        [HttpDelete("DeleteNotes")]
        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.DeleteNotes(noteid, userId);

                if (result == true)
                {
                    return this.Ok(new { Success = true, message = "Notes Deleted SuccessFully.", noteid });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Notes deletion failed." });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { success = false, message = e.Message, stackTrace = e.StackTrace });
            }

        }
        [HttpPut("Pin")]
        public IActionResult Ispinornot(long noteid)
        {
            try
            {
                var result = noteBL.IsPinORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Pinned Successfully", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        [HttpPut("Trash")]
        public IActionResult Istrashornot(long noteid)
        {
            try
            {
                var result = noteBL.IstrashORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note is in trash ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

       
        [HttpPut("Archive")]
        public IActionResult IsArchiveORNot(long noteid)
        {
            try
            {
                var result = noteBL.IsArchiveORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new { message = "Note Archived Successfully ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


       
        [HttpPut("Color")]
        public IActionResult Color(long noteid, string color)
        {
            try
            {
                var result = noteBL.Color(noteid, color);
                if (result != null)
                {
                    return this.Ok(new { message = "Color is changed ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Unable to change color" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
      
        [HttpPut("Upload")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.UploadImage(noteid,userId, img);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

