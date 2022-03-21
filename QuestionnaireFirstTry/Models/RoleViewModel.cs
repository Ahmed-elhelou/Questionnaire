using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace QuestionnaireFirstTry.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [NotMapped]
        [MinLength(1)]
        [NotNull]
        [Required]
        public string Name { get; set; }
        [NotMapped]
        [MinLength(1)]
        [NotNull]
        [Required]
        public  List<string> Claims { get; set; }
    }
}
