using BankBlazor.API.Context;
using BankBlazor.Client.Pages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankBlazor.Client.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        public AccountsController(BankBlazorContext context)
        {
            _context = context;
        }

       
        [HttpGet("{accountId}/balance")]
        public ActionResult<decimal> GetBalance(int accountId)
        {
            var account = _context.Accounts.Find(accountId);
            if (account == null) return NotFound();
            return account.Balance;
        }

       
        [HttpPost("deposit")]
        public ActionResult Deposit([FromBody] TransactionRequest request)
        {
            var account = _context.Accounts.Find(request.AccountId);
            if (account == null) return NotFound("Konto hittades inte.");
            account.Balance += request.Amount;

          
            var transaction = new Transaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                TransactionType = "Deposit",
                TransactionDate = DateTime.Now,
                Description = "Insättning"
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return Ok();
        }

       
        [HttpPost("withdraw")]
        public ActionResult Withdraw([FromBody] TransactionRequest request)
        {
            var account = _context.Accounts.Find(request.AccountId);
            if (account == null) return NotFound("Konto hittades inte.");
            if (account.Balance < request.Amount) return BadRequest("Otillräckligt saldo.");
            account.Balance -= request.Amount;

            var transaction = new Transaction
            {
                AccountId = request.AccountId,
                Amount = request.Amount,
                TransactionType = "Withdrawal",
                TransactionDate = DateTime.Now,
                Description = "Uttag"
            };
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
            return Ok();
        }

        
        [HttpPost("transfer")]
        public ActionResult Transfer([FromBody] TransferRequest request)
        {
            if (request.FromAccountId == request.ToAccountId)
                return BadRequest("Kan inte överföra till samma konto.");
            var fromAccount = _context.Accounts.Find(request.FromAccountId);
            var toAccount = _context.Accounts.Find(request.ToAccountId);
            if (fromAccount == null || toAccount == null) return NotFound("Ett eller flera konton hittades inte.");
            if (fromAccount.Balance < request.Amount) return BadRequest("Otillräckligt saldo för överföring.");

            fromAccount.Balance -= request.Amount;
            toAccount.Balance += request.Amount;

      
            _context.Transactions.Add(new Transaction
            {
                AccountId = request.FromAccountId,
                Amount = request.Amount,
                TransactionType = "Transfer",
                TransactionDate = DateTime.Now,
                Description = $"Överföring till konto {toAccount.AccountNumber}"
            });
            _context.Transactions.Add(new Transaction
            {
                AccountId = request.ToAccountId,
                Amount = request.Amount,
                TransactionType = "Transfer",
                TransactionDate = DateTime.Now,
                Description = $"Överföring från konto {fromAccount.AccountNumber}"
            });
            _context.SaveChanges();
            return Ok();
        }

      
        public class TransactionRequest
        {
            public int AccountId { get; set; }
            public decimal Amount { get; set; }
        }
        public class TransferRequest
        {
            public int FromAccountId { get; set; }
            public int ToAccountId { get; set; }
            public decimal Amount { get; set; }
        }
    }
}