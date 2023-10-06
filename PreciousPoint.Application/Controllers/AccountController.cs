using System.Net;
using System.Net.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager, IConfiguration configuration, IMapper mapper)
    {
      _userManager = userManager;
      _roleManager = roleManager;
      _configuration = configuration;
      _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterModel user)
    {
      if (user is not null && !string.IsNullOrEmpty(user.Email))
      {
        if (!await EmailAlreadyInUse(user.Email))
        {
          var newUser = _mapper.Map<User>(user);
          newUser.UserName = user.Email;
          var result = await _userManager.CreateAsync(newUser);
          if (!result.Succeeded) return BadRequest(result.Errors);
          var role = await GetRegistrationDefaultRole();
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
      var result = await _userManager.ConfirmEmailAsync(user, token);
      if (result.Succeeded) return Ok("Email successfully confirmed");
      else return StatusCode(500, "Unable to process the request");
    }

    //TODO This is temporary, will remove this in sometime. 
    [HttpGet("get-users-list")]
    public async Task<IEnumerable<User>> GetUsers()
    {
      return await _userManager.Users.ToListAsync();
    }

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

    //TODO Change it to actual implementation of default role. 
    private async Task<Role?> GetRegistrationDefaultRole()
    {
      return await _roleManager.FindByNameAsync("user");
    }

    private async Task<bool> EmailAlreadyInUse(string emailId)
    {
      return await _userManager.FindByEmailAsync(emailId) != null;
    }

  }
}