using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UserIdentity.EF.Dtos;
using UserIdentity.EF.Auth;

namespace UserIdentity.EF.Controllers
{
 
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtService _jwtService;
        private readonly RoleManager<IdentityRole> _roleManager;

        // Constructor that injects required services
        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            JwtService jwtService,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
            _roleManager = roleManager;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Create a new IdentityUser object
            var user = new IdentityUser { UserName = dto.Username, Email = dto.Email };

            // Create the user in the database with the given password
            var result = await _userManager.CreateAsync(user, dto.Password);

            // If the creation failed, return the errors
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // If the role does not exist yet, create it
            if (!await _roleManager.RoleExistsAsync(dto.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(dto.Role));
            }

            // Assign the new user to the specified role
            await _userManager.AddToRoleAsync(user, dto.Role);

            return Ok("User registered");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // Try to find the user by username
            var user = await _userManager.FindByNameAsync(dto.Username);

            // If user not found or password is wrong, return Unauthorized
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials");

            // Get the user's roles from the database
            var roles = await _userManager.GetRolesAsync(user);

            // Generate a JWT token for the user with their roles
            var token = _jwtService.GenerateToken(user, roles);

            // Return the token in the response
            return Ok(new { Token = token });
        }
    }

}
