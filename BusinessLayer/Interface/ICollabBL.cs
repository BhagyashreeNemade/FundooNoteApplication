﻿using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollabBL
    {
        public CollabEntity AddCollab(long noteid,long userid,string email);
        public bool Remove(long collabid);
        List<CollabEntity> GetAllByNoteID(long noteid);
    }
}