using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPCrud.Data;
using RPCrud.Models;

namespace RPCrud.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly RPCrud.Data.RPCrudContext _context;

        public IndexModel(RPCrud.Data.RPCrudContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Countries { get; set; }
        [BindProperty(SupportsGet = true)]
        public string UserCountry { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<string> countryQuery = from u in _context.User
                                            orderby u.Country
                                            select u.Country;

            var users = from u in _context.User
                         select u;
            if (!string.IsNullOrEmpty(SearchString))
            {
                users = users.Where(s => s.Login.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(UserCountry))
            {
                users = users.Where(x => x.Country == UserCountry);
            }
            Countries = new SelectList(await countryQuery.Distinct().ToListAsync());
            User = await users.ToListAsync();
        }
    }
}
