using System.ComponentModel;

namespace Account.Microservice.Core.Constants;
public enum ProductBlockOrder
{
  [Description("Overview")]
  Overview = 1,
  [Description("BlockImage")]
  Image = 2,
  [Description("Overview2")]
  Overview2 = 3,
  [Description("Status")]
  Status = 4,
  [Description("ButtonBuy")]
  ButtonBuy = 5,
  [Description("FreeText")]
  FreeInput = 6,
  [Description("LinkShare")]
  ShareLink = 7,
  [Description("FreeText2")]
  FreeInput2 = 8,
  [Description("Other")]
  Other = 9,
}
