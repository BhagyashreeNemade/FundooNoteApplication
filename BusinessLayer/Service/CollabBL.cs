using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BusinessLayer.Service
{
    public class CollabBL:ICollabBL

    {
        readonly ICollabRL iCollabRL;

        public CollabBL(ICollabRL iCollabRL)
        {
            this.iCollabRL = iCollabRL;
        }
        public CollabEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return this.iCollabRL.AddCollab(noteid, userid,email);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool Remove(long collabid)
        {
            try
            {
                return this.iCollabRL.Remove(collabid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<CollabEntity> RetriveDetails(long lableId)
        {
            try
            {
                return this.iCollabRL.RetriveDetails(lableId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
