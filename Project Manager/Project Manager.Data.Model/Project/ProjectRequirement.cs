using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
    public class ProjectRequirement : Base.ProjectBase
    {
        [Required]
        public string Description { get; set; }

        public int? Priority { get; set; }

    }
}