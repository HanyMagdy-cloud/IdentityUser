using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.EF.Interfaces;

namespace UserIdentity.EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogs _blogs;

        public BlogsController(IBlogs blogs)
        {
            _blogs = blogs;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs()
        {
            var blogs = await _blogs.GetAllAsync();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            var blog = await _blogs.GetbyIdAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return Ok(blog);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] Dtos.BlogDto blogDto)
        {
            if (blogDto == null)
            {
                return BadRequest("Blog data is null");
            }
            var createdBlog = await _blogs.CreateAsync(blogDto);
            return CreatedAtAction(nameof(GetBlogById), new { id = createdBlog.Id }, createdBlog);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] Dtos.BlogDto blogDto)
        {
            if (blogDto == null)
            {
                return BadRequest("Blog data is null");
            }
            var updatedBlog = await _blogs.UpdateAsync(blogDto, id);
            if (updatedBlog == null)
            {
                return NotFound();
            }
            return Ok(updatedBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var deletedBlog = await _blogs.DeleteAsync(id);
            if (deletedBlog == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchBlogs(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest("Keyword cannot be empty");
            }
            var blogs = await _blogs.SearchAsync(keyword);
            return Ok(blogs);

        }
    }
}
