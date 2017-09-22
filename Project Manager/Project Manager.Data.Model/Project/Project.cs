using Project_Manager.Data.Model.Finance;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
    public class Project : Base.ModelBase
    {
        public Guid? ParentProjectId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [ForeignKey(nameof(ParentProjectId))]
        public virtual Project ParentProject { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; }

        public virtual List<ProjectWorkFlow> WorkFlow { get; set; } = new List<ProjectWorkFlow>();

        public virtual List<ProjectWorkItem> WorkItems { get; set; } = new List<ProjectWorkItem>();

        public virtual List<ProjectComment> Comments { get; set; } = new List<ProjectComment>();
        public virtual List<ProjectNote> Notes { get; set; } = new List<ProjectNote>();
        public virtual List<ProjectTechnicalNote> TechnicalNotes { get; set; } = new List<ProjectTechnicalNote>();

        public virtual List<ProjectAssignedUsers> AssignedUsers { get; set; } = new List<ProjectAssignedUsers>();
        public virtual List<ProjectTagLink> Tags { get; set; } = new List<ProjectTagLink>();

        public virtual List<ProjectInvoiceLink> Invoices { get; set; } = new List<ProjectInvoiceLink>();

        public virtual List<ProjectRequirement> Requirements { get; set; } = new List<ProjectRequirement>();
        public virtual List<ProjectTest> Tests { get; set; } = new List<ProjectTest>();

        public virtual List<RelatedProjectLink> RelatedProjects { get; set; } = new List<RelatedProjectLink>();

    }
}