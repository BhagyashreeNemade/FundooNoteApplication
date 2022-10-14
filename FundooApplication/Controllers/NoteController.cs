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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace FundooApplication.Controllers
{
        [Authorize]
        [Route("api/[controller]")]
        [ApiController]
        public class NoteController : ControllerBase
        {
            INoteBL noteBL;
            FundooContext context;
        private readonly IMemoryCache memoryCache;
       
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<NoteController> logger;

        public NoteController(INoteBL noteBL, FundooContext context, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<NoteController> logger)
            {
                this.noteBL = noteBL;
                this.context = context;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }
        [HttpGet("Get")]
        public IActionResult GetNotes()
        {
            try
            {
                
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.GetAllNotes(userid);
                if (result != null)
                {
                    logger.LogInformation("Get User Notes Successfully");
                    return this.Ok(new { Success = true, message = "Get User Notes Successfully.", Data = result });
                }
                else
                {
                    logger.LogInformation("Unable to get note");
                    return this.BadRequest(new { Success = false, message = "Unable to get note" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        //[Authorize]
        [HttpPost("Add")]
            public IActionResult AddNotes(NoteModel addnote)
            {
                try
                {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                //long userid = User.FindFirst(ClaimTypes.UserId).Value;

                var result = noteBL.AddNote(addnote, userid);
                    if (result != null)
                    {
                    logger.LogInformation("Note Added Successfully");
                    return this.Ok(new { Success = true, message = "Note Added Successfully", result = result });
                    }
                    else
                    {
                    logger.LogInformation("Unable to add note");
                    return this.BadRequest(new { Success = false, message = "Unable to add note" });
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        [HttpPut("Update")]
        public IActionResult UpdateNotes(long noteid, NoteModel node)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.UpdateNotes(noteid, userId, node);

                if (result == true)
                {
                    logger.LogInformation("Notes Updated SuccessFully");
                    return this.Ok(new { Success = true, message = "Notes Updated SuccessFully", noteid });
                }
                else
                {
                    logger.LogInformation("Notes updation failed");
                    return this.BadRequest(new { Success = false, message = "Notes updation failed" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return this.BadRequest(new { success = false, message = ex.Message, stackTrace = ex.StackTrace });
            }

        }

        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.DeleteNotes(noteid, userId);

                if (result == true)
                {
                    logger.LogInformation("Notes Deleted SuccessFully");
                    return this.Ok(new { Success = true, message = "Notes Deleted SuccessFully.", noteid });
                }
                else
                {
                    logger.LogInformation("Notes deletion failed.");
                    return this.BadRequest(new { Success = false, message = "Notes deletion failed." });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return this.BadRequest(new { success = false, message = ex.Message });
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
                    logger.LogInformation("Note Pinned Successfully");
                    return this.Ok(new { message = "Note Pinned Successfully", Response = result });
                }
                else
                {
                    logger.LogInformation("Something Went Wrong");
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Note is in trash");
                    return this.Ok(new { message = "Note is in trash ", Response = result });
                }
                else
                {
                    logger.LogInformation("Something Went Wrong");
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Note Archived Successfully");
                    return this.Ok(new { message = "Note Archived Successfully ", Response = result });
                }
                else
                {
                    logger.LogInformation("Something Went Wrong");
                    return this.BadRequest(new { message = "Something Went Wrong" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Color is changed ");
                    return this.Ok(new { message = "Color is changed ", Response = result });
                }
                else
                {
                    logger.LogInformation("Unable to change color");
                    return this.BadRequest(new { message = "Unable to change color" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
      
        [HttpPut("UploadImage")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = noteBL.UploadImage(noteid,userId, img);
                if (result != null)
                {
                    logger.LogInformation("uploaded");
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    logger.LogInformation("Not uploaded");
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NoteList";
            string serializedNotesList;
            var NotesList = new List<NoteEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializedNotesList);
            }
            else
            {
                NotesList = await context.Notes.ToListAsync();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
        }
    }
}

