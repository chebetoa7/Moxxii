using Moxxii.mobile.Models.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services
{
    internal interface IFreeToPlayApi
    {
        [Get("/usuario")]
        Task<List<UsuarioResponse>> GetF2PAsync();
    }
}
