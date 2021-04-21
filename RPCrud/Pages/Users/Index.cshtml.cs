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

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public async Task OnGetAsync()
        {
            NameSort = String.IsNullOrEmpty(SortOrder) ? "login_desc" : "";
            DateSort = SortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<User> usersIQ = from u in _context.User
                                             select u;

            switch (SortOrder)
            {
                case "login_desc":
                    usersIQ = usersIQ.OrderByDescending(s => s.Login);
                    break;
                case "Date":
                    usersIQ = usersIQ.OrderBy(s => s.DateOfBirth);
                    break;
                case "date_desc":
                    usersIQ = usersIQ.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    usersIQ = usersIQ.OrderBy(s => s.Login);
                    break;
            }

            IQueryable<string> countryQuery = from u in _context.User
                                              orderby u.Country
                                              select u.Country;

            if (!string.IsNullOrEmpty(SearchString))
            {
                usersIQ = usersIQ.Where(s => s.Login.Contains(SearchString));
            }
            if (!string.IsNullOrEmpty(UserCountry))
            {
                usersIQ = usersIQ.Where(x => x.Country == UserCountry);
            }
            
            Countries = new SelectList(await countryQuery.Distinct().ToListAsync());
            User = await usersIQ.AsNoTracking().ToListAsync();
        }
    }
}
