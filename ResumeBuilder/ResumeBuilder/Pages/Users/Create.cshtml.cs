using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeBuilder.Data;
using ResumeBuilder.Models;
using ResumeBuilder.Pages;

namespace ResumeBuilder.Pages.Users
{
    public class CreateModel : PageModel
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

		public CreateModel(ResumeBuilder.Data.ResumeBuilderContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
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

				HttpContext.Session.SetString(SessionKeyId, emptyUser.Id.ToString());
				HttpContext.Session.SetString(SessionKeyEmail, emptyUser.Email);
				HttpContext.Session.SetString(SessionKeyUsername, emptyUser.Username);
				HttpContext.Session.SetString(SessionKeyFullName, emptyUser.FullName);
				HttpContext.Session.SetString(SessionKeyDateOfBirth, emptyUser.DateOfBirth.ToString().Remove(10));
				HttpContext.Session.SetString(SessionKeyPhoneNumber, emptyUser.PhoneNumber);
				HttpContext.Session.SetString(SessionKeyAddress, emptyUser.Address);

                return RedirectToPage("./AddMoreInfo");
            }

            return Page();
        }
    }
}
