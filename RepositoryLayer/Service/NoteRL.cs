using CommonLayer.Model;
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
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;
        public NoteRL(FundooContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this.config = config;
        }
        public List<NoteEntity> GetAllNotes(long userId)
        {
            try
            {
                var result = fundooContext.Notes.Where(e => e.UserId == userId).ToList();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NoteEntity AddNote(NoteModel node, long UserId)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.Title = node.Title;
                noteEntity.Note = node.Note;
                noteEntity.Reminder=node.Reminder;
                noteEntity.Color = node.Color;
                noteEntity.Image = node.Image;
                noteEntity.IsArchive = node.IsArchive;
                noteEntity.IsPin = node.IsPin;
                noteEntity.IsTrash = node.IsTrash;
                noteEntity.UserId = UserId;
                noteEntity.Createat = node.Createat;
                noteEntity.Modifiedat = node.Modifiedat;
                fundooContext.Notes.Add(noteEntity);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                return null;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool UpdateNotes(long noteid, long userId, NoteModel node)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteid && e.UserId == userId);

                if (result != null)
                {
                    if (node.Title != null)
                    {
                        result.Title = node.Title;
                    }
                    if (node.Note != null)
                    {
                        result.Note = node.Note;
                    }

                    result.Modifiedat = DateTime.Now;
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNotes(long noteid, long userId)
        {
            try
            {
                var result = fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteid && e.UserId == userId);

                if (result != null)
                {

                    fundooContext.Notes.Remove(result);
                    fundooContext.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
