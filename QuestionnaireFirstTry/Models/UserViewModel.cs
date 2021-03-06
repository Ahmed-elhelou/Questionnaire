using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuestionnaireFirstTry.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }

        [NotMapped]
        public List<string> Roles { get; set; }
    }
}
