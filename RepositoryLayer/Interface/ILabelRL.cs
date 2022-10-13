using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ILabelRL
    {
        public LabelEntity Addlabel(long noteid, long userid, string labels);
        public List<LabelEntity> GetlabelsByNoteid(long noteid, long userid);

        public bool RemoveLabel(long userID, long labelid);
        public bool RenameLabel(long userID, string oldLabelName, string labelName);
    }
}
