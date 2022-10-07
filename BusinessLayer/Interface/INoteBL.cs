using CommonLayer.Model;
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
    }
}
