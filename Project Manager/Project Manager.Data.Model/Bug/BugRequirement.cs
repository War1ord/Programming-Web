using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Bug
{
    public class BugRequirement : Base.BugBase
    {
        [Required]
        public string Description { get; set; }

        public int? Priority { get; set; }

    }
}