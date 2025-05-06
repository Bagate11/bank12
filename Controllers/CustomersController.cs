using BankBlazor.API.Context;
using BankBlazor.Client.Pages.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace BankBlazor.Client.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BankBlazorContext _context;
        public CustomersController(BankBlazorContext context)
        {
            _context = context;
        }

      
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null) return NotFound();
            return customer;
        }

       
        [HttpGet("{id}/accounts")]
        public ActionResult<IEnumerable<Account>> GetAccounts(int id)
        {
            var accounts = _context.Accounts.Where(a => a.CustomerId == id).ToList();
            return accounts;
        }
    }
}
