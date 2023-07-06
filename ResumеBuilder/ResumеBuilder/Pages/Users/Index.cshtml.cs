using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResumеBuilder.Data;
using ResumеBuilder.Models;

namespace ResumеBuilder.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly ResumеBuilder.Data.ResumеBuilderContext _context;

        public IndexModel(ResumеBuilder.Data.ResumеBuilderContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.User != null)
            {
                User = await _context.User.ToListAsync();
            }
        }
    }
}
