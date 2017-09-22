using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.ComplexTypes
{
    [ComplexType]
    public class Person
    {
        public DataTypes.Enums.Title? Title { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public DataTypes.Enums.Gender? Gender { get; set; }

        [NotMapped]
        public string FullNames
        {
            get
            {
                return string.Format("{0}{1}{2}"
                    , FirstName
                    , !string.IsNullOrWhiteSpace(MiddleName)
                        ? " " + MiddleName
                        : ""
                    , !string.IsNullOrWhiteSpace(LastName)
                        ? " " + LastName
                        : "");
            }
        }
    }
}