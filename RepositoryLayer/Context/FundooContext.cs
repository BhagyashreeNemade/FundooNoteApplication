using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
    : base(options)
        {
        }
        public DbSet<UserEntity> FundooDbTable { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<CollabEntity> CollabTable{ get; set; }
        public DbSet<LabelEntity> LabelsTable { get; set; }
    }
}
