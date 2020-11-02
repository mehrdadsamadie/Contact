using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Contact.Model
{
    public class ContactView
    {
        [Display(Name = "Contact Id")]
        public int ContactId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Image")]
        public string Image { get; set; }

        [Required]
        [Display(Name = "WebSite")]
        public string WebSite { get; set; }

        [Required]
        [Display(Name = "Note")]
        public string Note { get; set; }

        [Required]
        [Display(Name = "Sex")]
        public bool Gender { get; set; }

        [Display(Name = "Address Id")]
        public int AddressId { get; set; }

        [Required]
        [Display(Name = "Street Line1")]
        public string Street1 { get; set; }

        [Display(Name = "Street Line2")]
        public string Street2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"(^\d{5}(-\d{4})?$)|(^[ABCEGHJKLMNPRSTVXYabceghjklmnprstvxy]{1}\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstv‌​xy]{1} *\d{1}[ABCEGHJKLMNPRSTVWXYZabceghjklmnprstvxy]{1}\d{1}$)", ErrorMessage = "That postal code is not a valid US or Canadian postal code.")]
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }
        public string FullName { get; set; }
        public string ImageShow { get; set; }
    }
    public class ContactListView
    {
        public ContactListView()
        {
            List = new List<ContactView>();
        }
        public List<ContactView> List { get; set; }
        public int Total { get; set; }
    }
}
