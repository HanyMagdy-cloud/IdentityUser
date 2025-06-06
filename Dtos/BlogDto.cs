using System.ComponentModel.DataAnnotations;

namespace UserIdentity.EF.Dtos
{
    public class BlogDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Descreption { get; set; }

    }
}
