using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Reflection;
using UserInfo.Api.Models;
using UserInfo.Models;

namespace UserInfo.Web.Pages.Users
{
    public class CreateModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public CreateModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5028");
        }

        [BindProperty]
        public Models.User NewUser { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(NewUser);
                var stringContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("/api/Users/Create", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("ControlUsers");
                }
                else
                {
                    return Page();
                }
            }
            return Page();
        }
    }
}

