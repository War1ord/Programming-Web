using System;
using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Bug
{
    public class BugTest : Base.BugBase
    {
        [Required]
        public DateTime DateTimeTestedOn { get; set; }

        [Required]
        public DateTime DateTimeTestCompletedOn { get; set; }

        public string Note { get; set; }
    }
}