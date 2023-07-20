using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Models;

namespace ResumeBuilder.Pages
{
    public class ResumeTemp2Model : PageModel
    {
        private readonly ResumeBuilder.Data.ResumeBuilderContext _context;

        public ResumeTemp2Model(ResumeBuilder.Data.ResumeBuilderContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; } = default!;

        public async void OnGet()
        {
            User = await _context.User.FirstOrDefaultAsync(x => x.Id.ToString() == HttpContext.Session.GetString("_Id"));
        }
    }
}
