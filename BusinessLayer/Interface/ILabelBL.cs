using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ILabelBL
    {
        public LabelEntity Addlabel(long noteid, long userid, string labels);
        public List<LabelEntity> GetlabelsByNoteid(long noteid, long userid);
    }
}
