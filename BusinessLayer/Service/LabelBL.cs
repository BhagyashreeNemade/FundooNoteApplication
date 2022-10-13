using BusinessLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelbl;
        public LabelBL(ILabelRL labelbl)
        {
            this.labelbl = labelbl;
        }
        public LabelEntity Addlabel(long noteid, long userid, string labels)
        {
            try
            {
                return this.labelbl.Addlabel(noteid, userid, labels);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabelEntity> GetlabelsByNoteid(long noteid, long userid)
        {
            try
            {
                return this.labelbl.GetlabelsByNoteid(noteid, userid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool RemoveLabel(long userID, long labelid)
        {
            try
            {
                return this.labelbl.RemoveLabel(userID, labelid);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public bool RenameLabel(long userID, string oldLabelName, string labelName)
        {
            try
            {
                return this.labelbl.RenameLabel(userID, oldLabelName, labelName);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
