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
    }
}
