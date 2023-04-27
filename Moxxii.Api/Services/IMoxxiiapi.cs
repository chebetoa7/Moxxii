using Moxxii.Api.Body;
using Moxxii.Api.Models;
using Refit;

namespace Moxxii.Api.Services
{
    public interface IMoxxiiApi
    {
       

        [Get("/api/directions/json?origin={originLat},{originLon}&destination={deslat},{desLong}&key={key}")]
        Task<RouteMaps> GetRoute(double? latInitial, double? longInitial, double? latEnd, double? lonEnd, string key);

        [Get("/api/distancematrix/json?origin={originLat},{originLon}&destination={deslat},{desLong}&key={key}")]
        Task<RouteMaps> GetDistan(double originLat, double originLon,double deslat, double desLong, string key);


    }
}
