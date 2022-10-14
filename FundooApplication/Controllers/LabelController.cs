using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FundooApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        ILabelBL lables;
        private readonly IMemoryCache memoryCache;
        FundooContext context;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<LabelController> logger;


        public LabelController(ILabelBL lables, IMemoryCache memoryCache, FundooContext context, IDistributedCache distributedCache, ILogger<LabelController> logger)
        {
            this.lables = lables;
            this.memoryCache = memoryCache;
            this.context = context;
            this.distributedCache = distributedCache;
            this.logger = logger;
        }

        [HttpPost("Add")]
        public IActionResult AddLabels(long noteid, string labels)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = lables.Addlabel(noteid, userid, labels);
                if (result != null)
                {
                    logger.LogInformation("Labels Added Successfully");
                    return this.Ok(new { Success = true, message = "Labels Added Successfully", Response = result });
                }
                else
                {
                    logger.LogInformation("Unable to add");
                    return this.BadRequest(new { Success = false, message = "Unable to add" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                return this.BadRequest(new { Success = false, message = ex.Message });
            }
        }
        [HttpGet("Get")]
        public ActionResult<LabelEntity> GetByNoteid(long noteid)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = lables.GetlabelsByNoteid(noteid, userID);
                if (!result.Equals(null))
                {
                    logger.LogInformation("Labels by their NoteId");
                    return this.Ok(new
                    {
                        success = true,
                        message = "Labels by their NoteId",
                        data = result
                    });
                }
                else
                {
                    logger.LogInformation("something went wrong");
                    return this.BadRequest(new
                    {
                        success = false,
                        message = "something went wrong"
                    });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpDelete("Remove")]
        public IActionResult RemoveLabel(long labelid)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (lables.RemoveLabel(userID, labelid))
                {
                    logger.LogInformation("Label removed successfully");
                    return this.Ok(new { success = true, message = "Label removed successfully" });
                }
                else
                {
                    logger.LogInformation("User access denied");
                    return this.BadRequest(new { success = false, message = "User access denied" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("Rename")]
        public IActionResult RenameLabel(string lableName, string newLabelName)
        {
            try
            {
                long userID = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
                var result = lables.RenameLabel(userID, lableName, newLabelName);
                if (result != null)
                {
                    logger.LogInformation("Label renamed successfully");
                    return this.Ok(new { success = true, message = "Label renamed successfully", Response = result });
                }
                else
                {
                    logger.LogInformation("Unable to rename");
                    return this.BadRequest(new { success = false, message = "Unable to rename" });
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw;
            }
        }
        [HttpGet("RedisCache")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = await context.LabelsTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }

}