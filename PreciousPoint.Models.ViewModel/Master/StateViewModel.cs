using PreciousPoint.Models.ViewModel.BaseModel;

namespace PreciousPoint.Models.ViewModel.Master
{
  public class StateViewModel : BaseMasterViewModel
  {
    public CountryViewModel? Country { get; set; }
    public List<CityViewModel>? Cities { get; set; }
  }
}