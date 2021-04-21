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

        public PaginatedList<User> User { get; set; }

       // public IList<User> User { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Countries { get; set; }

        [BindProperty(SupportsGet = true)]
        public string UserCountry { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PageIndex { get; set; }

        public async Task OnGetAsync()
        {
            CurrentSort = SortOrder;
            NameSort = String.IsNullOrEmpty(SortOrder) ? "Login_desc" : "";
            DateSort = SortOrder == "Date" ? "Date_desc" : "Date";

            if (SearchString != null)
            {
                PageIndex = 1;
            }
            else
            {
                SearchString = CurrentFilter;
            }

            CurrentFilter = SearchString;

            IQueryable<User> usersIQ = from u in _context.User select u;

            switch (SortOrder)
            {
                case "Login_desc":
                    usersIQ = usersIQ.OrderByDescending(s => s.Login);
                    break;
                case "Date":
                    usersIQ = usersIQ.OrderBy(s => s.DateOfBirth);
                    break;
                case "Date_desc":
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
            var pageSize = Configuration.GetValue("PageSize", 4);
            Countries = new SelectList(await countryQuery.Distinct().ToListAsync());
            User = await PaginatedList<User>.CreateAsync(
                usersIQ.AsNoTracking(), PageIndex ?? 1, pageSize);
            //User = await usersIQ.AsNoTracking().ToListAsync();
        }
    }
}
