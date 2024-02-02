namespace Account.Microservice.Web.ApiModels.Medias;

public class MediaResponseModel : MediaModel
{
  public int PreviousMediaId { get; set; }
  public int NextMediaId { get; set; }
}
