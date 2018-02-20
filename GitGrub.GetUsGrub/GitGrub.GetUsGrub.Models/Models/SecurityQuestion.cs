﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.Models
{
    [Table("SecurityQuestion")]
    public class SecurityQuestion
    {
        [Key]
        public int Id { get; set; }
        
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required security question")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Required security question answer")]
        public string QuestionAnswer { get; set; }
    }
}
