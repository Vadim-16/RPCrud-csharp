using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RPCrud.Data;
using RPCrud.Models;
using Microsoft.Extensions.Configuration;

namespace RPCrud.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly RPCrudContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(RPCrudContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public PaginatedList<User> Users { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public SelectList Countries { get; set; }

        [BindProperty(SupportsGet = true)]
        public string UserCountry { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "Login_desc" : "";
            DateSort = sortOrder == "Date" ? "Date_desc" : "Date";

            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<User> usersIQ = from u in _context.User select u;

            usersIQ = sortOrder switch
            {
                "Login_desc" => usersIQ.OrderByDescending(s => s.Login),
                "Date" => usersIQ.OrderBy(s => s.DateOfBirth),
                "Date_desc" => usersIQ.OrderByDescending(s => s.DateOfBirth),
                _ => usersIQ.OrderBy(s => s.Login),
            };

            IQueryable<string> countryQuery = from u in _context.User
                                              orderby u.Country
                                              select u.Country;

            if (!string.IsNullOrEmpty(searchString))
            {
                usersIQ = usersIQ.Where(s => s.Login.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(UserCountry))
            {
                usersIQ = usersIQ.Where(x => x.Country == UserCountry);
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            Countries = new SelectList(await countryQuery.Distinct().ToListAsync());
            Users = await PaginatedList<User>.CreateAsync(
                usersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            
        }
    }
}
