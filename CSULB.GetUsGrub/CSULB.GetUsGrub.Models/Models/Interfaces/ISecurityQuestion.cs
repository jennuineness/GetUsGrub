﻿namespace CSULB.GetUsGrub.Models
{
    public interface ISecurityQuestion
    {
        string QuestionType { get; set; }

        string QuestionAnswer { get; set; }
    }
}
