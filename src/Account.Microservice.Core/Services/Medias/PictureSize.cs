using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Services.Medias;
public class PictureSize : IDisposable
{
  public int Width { get; set; }

  public int Height { get; set; }

  public string Name { get; set; } = string.Empty;

  public void Dispose()
  {

  }
}
