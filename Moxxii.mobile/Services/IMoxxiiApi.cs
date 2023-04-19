using Moxxii.mobile.Models.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxxii.mobile.Services
{
    [Headers( "Authorization: Bearer", "Content-Type: application/json;charset=utf-8")]
    public interface IMoxxiiApi
    {
        [Get("/Usuario/GetById?id={_id}")]
        Task<List<UsuarioResponse>> GetUsuario(int _id, [Header("Bearer")] string authorization);

        [Get("/Usuario/Get")]
        Task<List<UsuarioResponse>> GetUsuarioAll();
    }
}
