using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.Base
{
    /// <summary>
    /// Base model for all database models
    /// </summary>
    [Serializable]
    public abstract class IdModelBase
    {
        private DateTime? _created;
        private Result _result;
        private Guid _id;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id
        {
            get { return (_id == Guid.Empty ? (_id = Guid.NewGuid()) : _id); }
            set { _id = value; }
        }

        [NotMapped]
        public Result Result
        {
            get { return _result ?? (_result = new Result()); }
            set { _result = value ?? new Result(); }
        }
        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.DateTime)]
        public DateTime Created
        {
            get
            {
                return _created != null
                           ? _created.Value
                           : DateTime.Now;
            }
            set { _created = value; }
        }

        [Required, Display(Name = "Deleted", Description = "Deleted")]
        public bool IsDeleted { get; set; }
    }
}
