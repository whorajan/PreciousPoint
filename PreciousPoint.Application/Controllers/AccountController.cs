using System.Net.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreciousPoint.Application.Interfaces.Repository;
using PreciousPoint.Application.Interfaces.Services;
using PreciousPoint.Helpers.EmailCommunication;
using PreciousPoint.Models.DataModel.Account;
using PreciousPoint.Models.ViewModel.Account;

namespace PreciousPoint.Application.Controllers
{
  public class AccountController : BaseController
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration,
      IMapper mapper, IUserRepository userRepository, ITokenService tokenService)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _mapper = mapper;
      _userRepository = userRepository;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register([FromBody] RegisterModel user)
    {
      if (user is not null && !string.IsNullOrEmpty(user.Email))
      {
        if (!await _userRepository.EmailAlreadyExists(user.Email))
        {
          var newUser = _mapper.Map<User>(user);
          newUser.UserName = user.Email;
          var result = await _userManager.CreateAsync(newUser, user.Password);
          if (!result.Succeeded) return BadRequest(result.Errors);
          var role = await _userRepository.GetRegistrationDefaultRole();
          if (role is not null && !string.IsNullOrEmpty(role.Name))
          {
            result = await _userManager.AddToRoleAsync(newUser, role.Name);
            if (!result.Succeeded)
            {
              return BadRequest(result.Errors);
            }
          }
          var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
          var confirmationLink = Url.Action(nameof(ConfirmEmail), "account",
            new { token, email = user.Email }, Request.Scheme);
          if(!string.IsNullOrEmpty(confirmationLink) && !string.IsNullOrEmpty(newUser.Email))
          {
            Email email = new();
            bool emailResponse = await email.SendEmail(email.GetVerificationEmail(new MailAddress(newUser.Email),
                string.Join(' ', newUser.FirstName, newUser.MiddleName, newUser.LastName), confirmationLink, _configuration));
            if (emailResponse)
              return Ok("Registered successfully. Please verify email.");
            else
            {
              return StatusCode(500, "User was registered successfully. But system is failed to send activation link. Please retry after sometime");
            }
          }
          else
            return StatusCode(500, "User was registered successfully. But system is failed to send activation link. Please retry after sometime");
        }
        else
        {
          return Conflict("Email Id already taken");
        }
      }
      else
        return BadRequest("Invalid request.");
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
      var user = await _userManager.FindByEmailAsync(email);
      if (user is null) return NotFound();
      if (user.EmailConfirmed) return Ok("Email already verified");
      var result = await _userManager.ConfirmEmailAsync(user, token);
      if (result.Succeeded) return Ok("Email successfully verified");
      else return StatusCode(500, "Unable to process the request");
    }

    //TODO This is temporary, will remove this in sometime.
    [Authorize]
    [HttpGet("get-users-list")]
    public async Task<IEnumerable<User>> GetUsers()
    {
      return await _userManager.Users.ToListAsync();
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("role/{name}")]
    public async Task<ActionResult<Role>> CreateRole(string name)
    {
      var newRole = new Role
      {
        Name = name,
      };
      var result =  await _roleManager.CreateAsync(newRole);
      if (result.Succeeded) return Ok(newRole);
      else return BadRequest(result.Errors);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserViewModel>> Login([FromBody]LoginModel login)
    {
      var user = await _userManager.Users.SingleOrDefaultAsync(x => x.NormalizedUserName == login.UserName.ToUpper());
      if (user is null)
        return Unauthorized("User not found");
      var result = await _userManager.CheckPasswordAsync(user, login.Password);
      if (!result) return Unauthorized("Invalid user name or password");
      var userViewModel =  _mapper.Map<User, UserViewModel>(user);
      userViewModel.Token = await _tokenService.CreateToken(user);
      return Ok(userViewModel);
    }
  }
}