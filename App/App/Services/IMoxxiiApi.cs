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

        [Post("/api/Usuario/ShareUserFree")]
        Task<ResponseToken> GetShareFree(UserFreeBody dataLogin);

        [Get("/api/Usuario/updateToken?token={token}&id={id}")]
        Task<tokenUpdateResponse> UpdateToken(string token, int id);

        [Get("/api/Usuario/startjournal?disponibility_={disponibility}&id={id}")]
        Task<GenericResponse> StartDay(string disponibility, int id);

        [Post("/api/solicitud/solicitudNuevoViaje")]
        Task<SolicitudNuevoViajeResponse> SolicitudViaje(NuevoViaje solicitud);
        [Post("/api/solicitud/NuevoViaje")]
        Task<NuevoViajeResponse> SolicitudNuevoViaje(NuevoViaje solicitud);

        [Post("/api/Usuario/GetUserForTravel")]
        Task<NuevoViajeResponse> GetUserFree(UserFreeBody user);

        [Get("/api/Usuario/GetAvailability?city_={city_}")]
        Task<UsuarioResponse> GetUserDisponible(string city_);

        [Get("/api/Usuario/GetById?id={id}")]
        Task<ResponseToken> GetUserDisponibleByid(int id);

        [Post("/api/Usuario/GetLibre2")]
        Task<DataResponse> GetConductorLibre(Class1 user);

        [Post("/api/NotificationFirebase/Push")]
        Task<Responsemoxxii> SendPushFirebase(FCMBody fcm);
    }
}
