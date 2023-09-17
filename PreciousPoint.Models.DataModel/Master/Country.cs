using PreciousPoint.Models.DataModel.BaseModel;

namespace PreciousPoint.Models.DataModel.Master
{
  public class Country : BaseMasterModel
  {
    public List<State>? States { get; set; }
    public List<City>? Cities { get; set; }
  }
}