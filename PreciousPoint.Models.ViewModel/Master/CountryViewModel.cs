using PreciousPoint.Models.ViewModel.BaseModel;

namespace PreciousPoint.Models.ViewModel.Master
{
  public class CountryViewModel : BaseMasterViewModel
  {
    public List<StateViewModel>? States { get; set; }
    public List<CityViewModel>? Cities { get; set; }
  }
}