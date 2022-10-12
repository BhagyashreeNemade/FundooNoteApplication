using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FundooContext context;
        private readonly IConfiguration Iconfiguration;
        public LabelRL(FundooContext context, IConfiguration Iconfiguration)
        {
            this.context = context;
            this.Iconfiguration = Iconfiguration;
        }
        public LabelEntity Addlabel(long noteid, long userid, string label)
        {
            try
            {
                LabelEntity Entity = new LabelEntity();
                Entity.LabelName = label;
                Entity.UserId = userid;
                Entity.NoteID = noteid;
                this.context.LabelsTable.Add(Entity);
                int result = this.context.SaveChanges();
                if (result > 0)
                {
                    return Entity;
                }
                return null;

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
                var result = context.LabelsTable.Where(e => e.NoteID == noteid && e.UserId == userid).ToList();
            return result;
             }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveLabel(long userID, string labelName)
        {
            try
            {
                var result = this.context.LabelsTable.FirstOrDefault(x => x.UserId == userID && x.LabelName == labelName);
                if (result != null)
                {
                    context.Remove(result);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabelEntity> RenameLabel(long userID, string oldLabelName, string labelName)
        {
            IEnumerable<LabelEntity> labels;
            labels = context.LabelsTable.Where(x => x.UserId == userID && x.LabelName == oldLabelName).ToList();
            if (labels != null)
            {
                foreach (var newlabel in labels)
                {
                    newlabel.LabelName = labelName;
                }
                context.SaveChanges();
                return (List<LabelEntity>)labels;
            }
            return null;
        }
    }
}
