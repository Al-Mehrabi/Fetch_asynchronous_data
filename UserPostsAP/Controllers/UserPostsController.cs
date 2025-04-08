using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserPostsAPI.Models;

namespace UserPostsAPI.Controllers
{
    [ApiController]
    [Route("userandposts/{id}")]
    public class UserPostsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UserPostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPosts(int id)  
        {
            try
            {
                // Fetch user data based on the id
                var userResponse = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/users/{id}");

                // Check if the response is successful
                if (!userResponse.IsSuccessStatusCode)
                {
                    return NotFound($"External API error: User {id} not found");
                }

                
                var user = JsonConvert.DeserializeObject<User>(await userResponse.Content.ReadAsStringAsync());

                var postsResponse = await _httpClient.GetStringAsync($"https://jsonplaceholder.typicode.com/posts?userId={id}");
                var posts = JsonConvert.DeserializeObject<List<Post>>(postsResponse);

                var result = new
                {
                    user = new
                    {
                        user.Name,
                        user.Email,
                        user.Address,
                        user.Phone,
                        user.Website,
                        user.Company
                    },
                    posts
                };

                return Ok(result);
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
