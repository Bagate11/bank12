using BankBlazor.API.Context;
using BankBlazor.Client.Pages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Client.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        public TransactionsController(BankBlazorContext context)
        {
            _context = context;
        }

       
        [HttpGet("{accountId}")]
        public ActionResult<IEnumerable<Transaction>> GetByAccount(int accountId)
        {
            var transactions = _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
            return transactions;
        }
    }
}
