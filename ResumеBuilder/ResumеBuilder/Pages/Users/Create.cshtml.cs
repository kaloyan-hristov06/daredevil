using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumеBuilder.Data;
using ResumеBuilder.Models;

namespace ResumеBuilder.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly ResumеBuilder.Data.ResumеBuilderContext _context;

        public CreateModel(ResumеBuilder.Data.ResumеBuilderContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var emptyUser = new User();

            if (await TryUpdateModelAsync<User>(
                emptyUser,
                "user",
                s => s.Email, s => s.Username, s => s.FullName, s => s.Password, s => s.DateOfBirth, s => s.PhoneNumber, s => s.Address))
            {
                if (_context.User.Any(x => x.Email == emptyUser.Email)
                    || _context.User.Any(x => x.Username == emptyUser.Username))
                {
                    return Page();
                }
                _context.User.Add(emptyUser);
                await _context.SaveChangesAsync();
                return RedirectToPage($"./Details/{emptyUser.Id}");
            }

            return Page();
        }
    }
}
