using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class CollabEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabID { get; set; }
        public string CollabEmail { get; set; }
    

        [ForeignKey("FundooDbTable")]
        public long UserId { get; set; }
        [ForeignKey("Notes")]
        public long NoteID { get; set; }
        public virtual UserEntity userEntity { get; set; }
        public virtual NoteEntity noteEntity { get; set; }
    }
}
