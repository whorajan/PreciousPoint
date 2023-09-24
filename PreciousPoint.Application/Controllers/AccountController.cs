using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreciousPoint.Application.DataLayer;
using PreciousPoint.Models.DataModel.Account;
using PreciousPoint.Models.ViewModel.Account;

namespace PreciousPoint.Application.Controllers
{
  public class AccountController : BaseController
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public AccountController(UserManager<User> userManager, RoleManager<Role> roleManager)
    {
      _userManager = userManager;
      _roleManager = roleManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(RegisterModel user)
    {
      if (user is not null)
      {
        if (!await EmailAlreadyInUse(user.Email))
        {
          var newUser = new User
          {
            FirstName = user.FirstName,
            MiddleName = user.MiddleName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNo,
            UserName = user.Email,
          };
          var result = await _userManager.CreateAsync(newUser);
          if(!result.Succeeded) return BadRequest(result.Errors);
          var role = await GetRegistrationDefaultRole();
          if (role is not null)
          {
            result = await _userManager.AddToRoleAsync(newUser, role.Name??"");
            if(!result.Succeeded)
            {
              return BadRequest(result.Errors);
            }
          }
          return Ok(newUser);
        }
        else
        {
          return Conflict("Email Id already taken");
        }
      }
      else
        return BadRequest("Invalid request.");
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