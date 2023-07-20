using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Data;
using ResumeBuilder.Models;

namespace ResumeBuilder.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly ResumeBuilder.Data.ResumeBuilderContext _context;

        public const string SessionKeyId = "_Id";
        public const string SessionKeyEmail = "_Email";
        public const string SessionKeyUsername = "_Username";
        public const string SessionKeyFullName = "_FullName";
        public const string SessionKeyDateOfBirth = "_DateOfBirth";
        public const string SessionKeyPhoneNumber = "_PhoneNumber";
        public const string SessionKeyAddress = "_Address";

        //logs a session
        private readonly ILogger<IndexModel> _logger;

        public EditModel(ResumeBuilder.Data.ResumeBuilderContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var existingUser = await _context.User.FindAsync(User.Id);

            if (existingUser == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<User>(
                existingUser,
                nameof(User),
                u => u.Email, u => u.PhoneNumber, u => u.Address, u => u.Username, u => u.Password, u => u.DateOfBirth, u => u.FullName,
                u => u.Skills, u => u.Interests, u => u.Experiences, u => u.Education))
            {
                try
                {
                    HttpContext.Session.SetString(SessionKeyId, User.Id.ToString());
                    HttpContext.Session.SetString(SessionKeyEmail, User.Email);
                    HttpContext.Session.SetString(SessionKeyUsername, User.Username);
                    HttpContext.Session.SetString(SessionKeyFullName, User.FullName);
                    HttpContext.Session.SetString(SessionKeyDateOfBirth, User.DateOfBirth.ToString().Remove(10));
                    HttpContext.Session.SetString(SessionKeyPhoneNumber, User.PhoneNumber);
                    HttpContext.Session.SetString(SessionKeyAddress, User.Address);

                    await _context.SaveChangesAsync();

                    return Redirect($"/Users/Details/{User.Id}");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(existingUser.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return RedirectToPage("/Index");
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
