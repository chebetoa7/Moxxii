using App.Models.Body;
using App.Response;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Services
{
    [Headers("Authorization: Bearer", "Content-Type: application/json;charset=utf-8")]
    public interface IMoxxiiApi
    {
        [Get("/api/Usuario/GetById?id={_id}")]
        Task<List<UsuarioResponse>> GetUsuario(int _id, [Header("Bearer")] string authorization);

        [Get("/api/Usuario/GetById?id={_id}")]
        Task<List<UsuarioResponse>> GetUsuario2(int _id);

        [Get("/api/Usuario/Get")]
        Task<List<UsuarioResponse>> GetUsuarioAll();

        [Post("/api/Usuario/token")]
        Task<ResponseToken> GetTokenUser(loginModel dataLogin);
    }
}
