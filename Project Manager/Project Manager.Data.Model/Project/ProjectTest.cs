using System;
using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
    public class ProjectTest : Base.ProjectBase
    {
        [Required]
        public DateTime DateTimeTestedOn { get; set; }

        [Required]
        public DateTime DateTimeTestCompletedOn { get; set; }

        public string Note { get; set; }
    }
}