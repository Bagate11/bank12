using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    namespace BankBlazor.Client.Pages.Models
{
        public class Customer
        {
            [Key]
            public int CustomerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PersonalIdNumber { get; set; }

            public ICollection<Account> Accounts { get; set; }
        }
    }

