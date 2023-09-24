using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreciousPoint.Application.DataLayer;
using PreciousPoint.Models.DataModel.Account;

namespace PreciousPoint.Application.Controllers
{
  public class CountryController : BaseController
  {
    private readonly BaseDataContext _context;

    public CountryController(BaseDataContext context)
    {
      _context = context;
    }
  }
}