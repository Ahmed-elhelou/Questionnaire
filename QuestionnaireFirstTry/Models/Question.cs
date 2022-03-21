using System;
using System.ComponentModel.DataAnnotations;

namespace QuestionnaireFirstTry.Models
{
    public class Question
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string QuestionBody { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Option1 { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Option2 { get; set; }
        [Required]
        [MaxLength(250)]
        [MinLength(1)]
        public string Option3 { get; set; }
        public DateTime InsertDate { get; set; }
        public bool Active { get; set; }
    }
}
