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
    public class CollabController : ControllerBase
    {
        readonly ICollabBL collabBL;
        private readonly IMemoryCache memoryCache;
        FundooContext context;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<CollabController> logger;

        public CollabController(ICollabBL collabBL, IMemoryCache memoryCache, FundooContext context, IDistributedCache distributedCache, ILogger<CollabController> logger)
        {
            this.collabBL = collabBL;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
            this.logger = logger;

        }
        [HttpPost("Add")]
        public IActionResult AddCollab(long noteid, string email)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);


                var result = collabBL.AddCollab(noteid, userid, email);
                if (result != null)
                {
                    logger.LogInformation("Collaborator Added Successfully");
                    return this.Ok(new { Success = true, message = "Collaborator Added Successfully", Response = result });
                }
                else
                {
                    logger.LogInformation("Unable to add Collaborator");
                    return this.BadRequest(new { Success = false, message = "Unable to add Collaborator" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
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
                    logger.LogInformation("Deleted Successfully");
                    return this.Ok(new { Success = true, message = "Deleted Successfully" });
                }
                else
                {
                    logger.LogInformation("Unable to Delete");
                    return this.BadRequest(new { Success = false, message = "Unable to Delete" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("GetDetails")]
        public IActionResult RetriveDetails(long noteId)
        {
            try
            {
                var result = collabBL.RetriveDetails(noteId);
                if (!result.Equals(null) && !result.Count.Equals(0))
                {
                    logger.LogInformation("Retrive collaborator sucessfully");
                    return this.Ok(new
                    {

                        success = true,
                        message = "Retrive collaborator sucessfully",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("Data Not Found");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "Data Not Found"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllCollabsUsingRedisCache()
        {
            var cacheKey = "CollabList";
            string serializedCollabList;
            var CollabList = new List<CollabEntity>();
            var redisCollabList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabList != null)
            {
                serializedCollabList = Encoding.UTF8.GetString(redisCollabList);
                CollabList = JsonConvert.DeserializeObject<List<CollabEntity>>(serializedCollabList);
            }
            else
            {
                CollabList = await context.CollabTable.ToListAsync();
                serializedCollabList = JsonConvert.SerializeObject(CollabList);
                redisCollabList = Encoding.UTF8.GetBytes(serializedCollabList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabList, options);
            }
            return Ok(CollabList);
        }
    }

}
