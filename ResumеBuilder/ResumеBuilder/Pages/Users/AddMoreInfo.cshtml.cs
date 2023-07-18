using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ResumеBuilder.Models;

namespace ResumеBuilder.Pages.Users
{
    public class AddMoreInfoModel : PageModel
    {
        private readonly ResumеBuilder.Data.ResumеBuilderContext _context;

        public AddMoreInfoModel(ResumеBuilder.Data.ResumеBuilderContext context)
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

			string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ResumеBuilderContext-68046c86-919c-453f-8cae-e66aa7b85116;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE [dbo].[User] SET [Skills] = @Skills, [Interests] = @Interests, [Experiences] = @Experiences, [Education] = @Education WHERE [Id] = @ItemId";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Skills", skills);
                    command.Parameters.AddWithValue("@Interests", interests);
                    command.Parameters.AddWithValue("@Experiences", experiences);
                    command.Parameters.AddWithValue("@Education", education);
					command.Parameters.AddWithValue("@ItemId", int.Parse(HttpContext.Session.GetString("_Id")));

                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return Redirect($"./Details/{HttpContext.Session.GetString("_Id")}");
        }
    }
}
