using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.User
{
    public class User : Base.ModelBase
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}