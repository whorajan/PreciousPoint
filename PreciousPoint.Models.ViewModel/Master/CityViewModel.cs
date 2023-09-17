using PreciousPoint.Models.ViewModel.BaseModel;

namespace PreciousPoint.Models.ViewModel.Master
{
  public class CityViewModel : BaseMasterViewModel
  {
    public StateViewModel? State { get; set; }
    public CountryViewModel? Country { get; set; }
  }
}