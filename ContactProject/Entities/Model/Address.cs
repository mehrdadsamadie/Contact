using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Entities.Model
{
    [Table("Address", Schema = "CNT")]
    public class Address
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Address Id")]
        public int AddressId { get; set; }

        [ForeignKey("FK_ContactInfos_Adress")]
        [Display(Name = "Contact Id")]
        public int ContactId { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        [Display(Name = "Street Line1")]
        public string Street1 { get; set; }

        [Column(TypeName = "varchar(256)")]
        [Display(Name = "Street Line2")]
        public string Street2 { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "Postal Code")]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)", ErrorMessage = "That postal code is not a valid US or Canadian postal code.")]
        public string PostalCode { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
    }

}
