using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ResumeBuilder.Models;

namespace ResumеBuilder.Pages.Users
{
    public class AddMoreInfoModel : PageModel
    {
        private readonly ResumeBuilder.Data.ResumeBuilderContext _context;

        public AddMoreInfoModel(ResumeBuilder.Data.ResumeBuilderContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        private string CreateXString(string className)
        {
			Dictionary<string, Microsoft.Extensions.Primitives.StringValues> dict = HttpContext.Request.Form.Where(x => x.Key == className).ToDictionary(x => x.Key, x => x.Value);

			string str = string.Empty;

			foreach (var item in dict)
			{
				str += item.Value;
			}

            return str;
		}

        public async Task<IActionResult> OnPostAsync()
        {
            if(HttpContext.Session.GetString("_Id") == default(string))
            {
                return Page();
            }

            string skills = CreateXString("skillField");

            string interests = CreateXString("interestField");

            string experienceFromDates = CreateXString("experienceFromDate");

            string experienceToDates = CreateXString("experienceToDate");

            string experienceJobTitles = CreateXString("experienceJobTitle");

            string experienceCompanyNames = CreateXString("experienceCompanyName");

            string experienceCities = CreateXString("experienceCity");

            string experienceCountries = CreateXString("experienceCountry");

            string graduated = CreateXString("graduated");

            string educationDegree = CreateXString("educationDegree");

            string educationStudyPlace = CreateXString("educationStudyPlace");

            string educationCity = CreateXString("educationCity");

            string educationCountry = CreateXString("educationCountry");

            string experiences = String.Concat(experienceFromDates, "|", experienceToDates, "|", experienceJobTitles, "|", experienceCompanyNames, "|", experienceCities, "|", experienceCountries);
            string education = String.Concat(graduated, "|", educationDegree, "|", educationStudyPlace, "|", educationCity, "|", educationCountry);

			string connectionString = "Data Source=ResumeBuilderContext-cbd36869-b7a2-44e3-bfd1-27daa68f13bf.db";

            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE [User] SET [Skills] = @Skills, [Interests] = @Interests, [Experiences] = @Experiences, [Education] = @Education WHERE [Id] = @ItemId";

                using (SqliteCommand command = new SqliteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Skills", skills);
                    command.Parameters.AddWithValue("@Interests", interests);
                    command.Parameters.AddWithValue("@Experiences", experiences);
                    command.Parameters.AddWithValue("@Education", education);
					command.Parameters.AddWithValue("@ItemId", Convert.ToInt32(HttpContext.Session.GetString("_Id")));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return Redirect($"./Details/{HttpContext.Session.GetString("_Id")}");
        }
    }
}
