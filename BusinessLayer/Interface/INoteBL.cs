using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity AddNote(NoteModel node, long NoteEntity);
        List<NoteEntity> GetAllNotes(long userId);
        public bool UpdateNotes(long noteid, long userId, NoteModel node);

        public bool DeleteNotes(long noteid, long userId);
        public bool IsPinORNot(long noteid);
        public bool IsArchiveORNot(long noteid);
        public bool IstrashORNot(long noteid);

        public NoteEntity Color(long noteid, string color);
        public string UploadImage(long noteid, long userId, IFormFile img);
    }
}
