using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RepositoryLayer.Service
{
    public class CollabRL: ICollabRL
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;

        public CollabRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)

        {
            try
            {
                CollabEntity collabEntity = new CollabEntity();
                collabEntity.CollabEmail = email;
                collabEntity.UserId = userid;
                collabEntity.NoteID = noteid;
                collabEntity.Modifiedat = DateTime.Now;
                fundooContext.CollabTable.Add(collabEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return collabEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool Remove(long collabid)
        {
            try
            {
                var result = this.fundooContext.CollabTable.FirstOrDefault(x => x.CollabID == collabid);
                fundooContext.Remove(result);
                int deletednote = this.fundooContext.SaveChanges();
                if (deletednote > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
