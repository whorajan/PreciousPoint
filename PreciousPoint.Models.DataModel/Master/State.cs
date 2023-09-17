using PreciousPoint.Models.DataModel.BaseModel;

namespace PreciousPoint.Models.DataModel.Master
{
  public class State : BaseMasterModel
  {
    public Country? Country { get; set; }
    public List<City>? Cities { get; set; }
  }
}