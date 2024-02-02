using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Data;
using Account.Microservice.Core.Entities.UserAggregate;

namespace Account.Microservice.Core.Services.Medias;
public interface IMediaService
{
  Task<Media> UploadMedia(byte[] mediaBinary, string mimeType, string seoFilename,
            int createdUserId,
            long contentLength,
            string altAttribute, string titleAttribute,
            int mediType, bool validateBinary = true);
  Task<Media?> UploadMedia(byte[] mediaBinary, string mimeType, string seoFilename,
          int createdUserId,
          long contentLength,
          string altAttribute, string titleAttribute,
          string keyName);
  Task<bool> UpdateUrl(int mediaId, string url);
  Task<IPagedList<Media>> GetListByUserId(int page, int count, int? userId = null, bool selectOnlyImage = false, int? userAdminId = 0);
  Task<Media> GetById(int id);
  Task HideMedia(int id);
}
