using Project_Manager.Data.Model.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Bug
{
    public class Bug : Base.ModelBase
    {
        public Guid? ParentBugId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public string Resolution { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(ParentBugId))]
        public virtual Bug ParentBug { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }

        public virtual List<RelatedBugLink> RelatedBugs { get; set; } = new List<RelatedBugLink>();

        public virtual List<BugComment> Comments { get; set; } = new List<BugComment>();
        public virtual List<BugNote> Notes { get; set; } = new List<BugNote>();
        public virtual List<BugTechnicalNote> TechnicalNotes { get; set; } = new List<BugTechnicalNote>();

        public virtual List<BugAssignedUsers> AssignedUsers { get; set; } = new List<BugAssignedUsers>();
        public virtual List<BugTagLink> Tags { get; set; } = new List<BugTagLink>();

        public virtual List<BugRequirement> Requirements { get; set; } = new List<BugRequirement>();
        public virtual List<BugTest> Tests { get; set; } = new List<BugTest>();

    }
}
