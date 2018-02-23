﻿using GitGrub.GetUsGrub.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GitGrub.GetUsGrub.DataAccess
{
    /// <summary>
    /// A duplicate security question entity to abstract the ORM framework.
    /// 
    /// Author: Brian Fann
    /// Last Updated: 2/21/18
    /// </summary>
    [Table("GetUsGrub.SecurityQuestion")]
    public class SecurityQuestionEntity : ISecurityQuestion
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserAccount")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Required security question")]
        public string QuestionType { get; set; }

        [Required(ErrorMessage = "Required security question answer")]
        public string QuestionAnswer { get; set; }

        // NAVIGATION
        public UserAccountEntity UserAccount { get; set; }
    }
}