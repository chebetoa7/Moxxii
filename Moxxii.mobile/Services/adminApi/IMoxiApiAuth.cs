using Moxxii.mobile.Models.Body;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services.adminApi
{
    public interface IMoxiApiAuth
    {
        [Post("/api/Usuario/Login")]
        Task<HttpResponseMessage> GetAppToken([Body(BodySerializationMethod.UrlEncoded)] loginModel loginModelTokent);
    }
}
