using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public async Task OnGetAsync()
        {
            User = await _context.User.ToListAsync();
        }
    }
}
