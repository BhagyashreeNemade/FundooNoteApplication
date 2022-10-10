using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NoteModel node, long NoteEntity);
        List<NoteEntity> GetAllNotes(long userId);
        public bool UpdateNotes(long noteid, long userId, NoteModel node);

        public bool DeleteNotes(long noteid, long userId);
        public NoteEntity IsPinORNot(long noteid);
        public NoteEntity IsArchiveORNot(long noteid);
        public NoteEntity IstrashORNot(long noteid);
        public NoteEntity Color(long noteid, string color);

    }
}
