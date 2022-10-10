using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NoteBL : INoteBL
    {
        INoteRL iNoteRL;
        public NoteBL(INoteRL iNoteRL)
        {
            this.iNoteRL = iNoteRL;
        }

        public NoteEntity AddNote(NoteModel node, long UserId)
        {
            try
            {
                return this.iNoteRL.AddNote(node, UserId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UpdateNotes(long noteid, long userId, NoteModel node)
        {
            try
            {
                return this.iNoteRL.UpdateNotes(noteid, userId, node);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNotes(long id, long userId)
        {
            try
            {
                return this.iNoteRL.DeleteNotes(id, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                return iNoteRL.GetAllNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool IsPinORNot(long noteid)
        {
            try
            {
                return iNoteRL.IsPinORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IsArchiveORNot(long noteid)
        {
            try
            {
                return iNoteRL.IsArchiveORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IstrashORNot(long noteid)
        {
            try
            {
                return iNoteRL.IstrashORNot(noteid);
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        public NoteEntity Color(long noteid, string color)
        {
            try
            {
                return this.iNoteRL.Color(noteid, color);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadImage(long noteid, long userId, IFormFile img)
        {
            try
            {
                return this.iNoteRL.UploadImage(noteid,userId, img);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
