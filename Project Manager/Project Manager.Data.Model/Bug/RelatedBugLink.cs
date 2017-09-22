using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Bug
{
    public class RelatedBugLink : Base.BugBase
    {
        [Required]
        public Guid RelatedBugId { get; set; }

        [ForeignKey(nameof(RelatedBugId))]
        public virtual Bug RelatedBug { get; set; }
    }
}