using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BankBlazor.Client.Pages.Models
{
    public class Transaction
    {
        
        public int TransactionId { get; set; }

        public int AccountId { get; set; }
        public Account Account { get; set; }

        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
    }
}