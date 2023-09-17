using PreciousPoint.Models.DataModel.BaseModel;

namespace PreciousPoint.Models.DataModel.Master
{
  public class City : BaseMasterModel
  {
    public State? State { get; set; }
    public Country? Country { get; set; }
  }
}