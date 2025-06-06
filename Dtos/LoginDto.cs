using System.ComponentModel.DataAnnotations;

namespace UserIdentity.EF.Dtos
{
    
    public class LoginDto
    {
        public required string Username { get; set; }

        public required string Password { get; set; }
    }
}
