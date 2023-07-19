using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ResumeBuilder.Pages.Users
{
    public class LoginModel : PageModel
    {
		//database context aka the thing that allows us to access all users
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

		public LoginModel(ResumeBuilder.Data.ResumeBuilderContext context, ILogger<IndexModel> logger)
		{
			_context = context;
			_logger = logger;
		}

		//Receives the post request and stores the information if correct in an active session
		[HttpPost]
		public async Task<IActionResult> OnPostAsync(string email, string password, string username)
		{
			var user = await _context.User.FirstOrDefaultAsync(x => x.Email == email);

			if (user != null && user.Username == username && user.Password == password)
			{
				HttpContext.Session.SetString(SessionKeyId, user.Id.ToString());
				HttpContext.Session.SetString(SessionKeyEmail, user.Email);
                HttpContext.Session.SetString(SessionKeyUsername, user.Username);
				HttpContext.Session.SetString(SessionKeyFullName, user.FullName);
				HttpContext.Session.SetString(SessionKeyDateOfBirth, user.DateOfBirth.ToString().Remove(10));
                HttpContext.Session.SetString(SessionKeyPhoneNumber, user.PhoneNumber);
                HttpContext.Session.SetString(SessionKeyAddress, user.Address);

                return RedirectToPage("/LoginSuccess");
			}
			else
			{
				ModelState.AddModelError(string.Empty, "Invalid email or password");
                return Page();
			}
		}
	}
}
