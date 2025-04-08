using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserPostsAPI.Models;

namespace UserPostsAPI.Controllers
{
    [ApiController]
  [Route("userandposts")]
    public class UserPostsController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public UserPostsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserPosts()
        {
            try
            {
                var userResponse = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/users/1");
                var user = JsonConvert.DeserializeObject<User>(userResponse);

                var postsResponse = await _httpClient.GetStringAsync("https://jsonplaceholder.typicode.com/posts?userId=1");
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
