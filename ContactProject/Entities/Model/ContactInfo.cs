using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Entities.Model
{
    [Table("ContactInfo", Schema = "CNT")]
    public class ContactInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Contact Id")]
        public int ContactId { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "varchar(128)")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "varchar(15)")]
        [Display(Name = "Phone")]
        public string Phone  { get; set; }

        [Required]
        [Column(TypeName = "varchar(500)")]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Column(TypeName = "varchar(256)")]
        [Display(Name = "WebSite")]
        public string WebSite { get; set; }

        [Required]
        [Column(TypeName = "varchar(1000)")]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public bool Gender { get; set; }

        [NotMapped]
        public string FullName => $"{Name} {LastName}";
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
