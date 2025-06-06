using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserIdentity.EF.Data;
using UserIdentity.EF.Dtos;
using UserIdentity.EF.Interfaces;
using UserIdentity.EF.Model;

namespace UserIdentity.EF.Repos
{
    public class BlogsRepos : IBlogs
    {
        private readonly ApplicationDbContext _context;

        public BlogsRepos(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Blog> CreateAsync(BlogDto blogdto)
        {
            var blog = new Blog
            {
                Name = blogdto.Name,
                Descreption = blogdto.Descreption,
            };

            _context.blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog?> DeleteAsync(int id)
        {
            var blig = _context.blogs.FindAsync(id);

            if (blig == null)
            {
                return null;
            }
             _context.blogs.Remove(await blig);
            await _context.SaveChangesAsync();
            return await blig;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.blogs.ToListAsync();

        }

        public async Task<Blog?> GetbyIdAsync(int id)
        {
            var blog = _context.blogs.FindAsync(id);

            if (blog == null)
            {
                return null;
            }
            return await blog;
        }

        public async Task<Blog?> UpdateAsync(BlogDto blogdto, int id)
        {
            var blog = await _context.blogs.FindAsync(id);

            if (blog == null)
            {
                return null;
            }
            blog.Name = blogdto.Name;
            blog.Descreption = blogdto.Descreption;

            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<IEnumerable<Blog>> SearchAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return await _context.blogs.ToListAsync();

            var loweredKeyword = keyword.ToLower();

            return await _context.blogs
                .Where(b => b.Name.ToLower().Contains(loweredKeyword) ||
                            b.Descreption.ToLower().Contains(loweredKeyword))
                .ToListAsync();
        }
    }
}
