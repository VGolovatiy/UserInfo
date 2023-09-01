using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace UserInfo.Web.Pages.Users
{
    public class ControlUsersModel : PageModel
    {

        private readonly HttpClient _httpClient;

        public ControlUsersModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5028");
        }

        public IEnumerable<Models.User> users { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _httpClient.GetAsync("/api/Users/GetAll");

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                users = JsonConvert.DeserializeObject<IEnumerable<Models.User>>(jsonResponse);
            }
            else
            {
                users = new List<Models.User>();
            }

        }
        public async Task<IActionResult> OnPostDeleteUserAsync(int id)
        {
            var response = await _httpClient.PostAsync($"/api/Users/Delete/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Users/ControlUsers");
            }
            else
            {
                return Page();
            }
        }
    }
}
