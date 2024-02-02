using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public enum SurveyTypeEnum
{
  [Description("テキスト")]
  Text,
  [Description("テキストエリア")]
  TextArea,
  [Description("チェックボックス")]
  CheckBox,
  [Description("ラジオボタン")]
  RadioButton,
  [Description("プルダウン")]
  DropdownList,
}
