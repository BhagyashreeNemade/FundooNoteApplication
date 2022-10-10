using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;

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
        public bool IsPinORNot(long noteid)
        {
            try
            {
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsPin == true)
                {
                    result.IsPin = false;
                    this.fundooContext.SaveChanges();
                    return false;
                }
                else
                {
                    result.IsPin = true;
                    this.fundooContext.SaveChanges();
                    return true;
                }
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
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsArchive == true)
                {
                    result.IsArchive = false;
                    this.fundooContext.SaveChanges();
                    return false;
                }
                result.IsArchive = true;
                this.fundooContext.SaveChanges();
                return true;
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
                NoteEntity result = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (result.IsTrash == true)
                {
                    result.IsTrash = false;
                    this.fundooContext.SaveChanges();
                    return false;
                }
                result.IsTrash = true;
                this.fundooContext.SaveChanges();
                return true;
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
                NoteEntity note = this.fundooContext.Notes.FirstOrDefault(x => x.NoteID == noteid);
                if (note.Color != null)
                {
                    note.Color = color;
                    this.fundooContext.SaveChanges();
                    return note;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string UploadImage(long noteid, long userId,IFormFile img)
        {
            try
            {
                var result = this.fundooContext.Notes.FirstOrDefault(e => e.NoteID == noteid && e.UserId==userId);
                if (result != null)
                {
                    Account account = new Account(
                     this.config["CloudinarySettings:CloudName"],
                       this.config["CloudinarySettings:ApiKey"],
                        this.config["CloudinarySettings:ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, img.OpenReadStream()),
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    string imagePath = uploadResult.Url.ToString();
                    result.Image = imagePath;
                    fundooContext.SaveChanges();
                    return "Image uploaded successfully";
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
