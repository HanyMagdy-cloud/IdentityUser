using UserIdentity.EF.Dtos;
using UserIdentity.EF.Model;

namespace UserIdentity.EF.Interfaces
{
    public interface IBlogs
    {
        Task<IEnumerable<Blog>> GetAllAsync();
        Task<Blog?> GetbyIdAsync(int id);

        Task<Blog> CreateAsync(BlogDto blog);

        Task<Blog?> UpdateAsync(BlogDto blogdto, int id);
        Task<Blog?> DeleteAsync(int id);

        // Search property
        Task<IEnumerable<Blog>> SearchAsync(string keyword);


    }
}
