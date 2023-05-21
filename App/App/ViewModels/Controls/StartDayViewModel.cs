using App.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.ViewModels.Controls
{
   
    public partial class StartDayViewModel: BaseViewModel
    {
        public StartDayViewModel()
        {
            managerServices = new Services.ManagerServices();
            OkStartDay = new Command(async () => await TapStartDayCommands());
        }

        private ManagerServices managerServices;

        public Command OkStartDay { get; }

        private async Task TapStartDayCommands()
        {
            try 
            {
                var id_ = Preferences.Get("config_user_id",0);
                //var id_Config = id_.ToString();
                var access_token_Config = Preferences.Get("config_user_tokenApi", "");
                var startResponse = await managerServices.StartDay("libre", id_, access_token_Config);

                if (startResponse)
                {
                    await DisplayAlertMessage("Activo para viaje", "Ahora ya estas listo para recibir viajes, te pedimos que seas responsable cuando aceptes viaje", "OK");
                    await TerminaRutaPopup();
                }
                else 
                {
                    await DisplayAlertMessage("Error al iniciar día", "Existe un error de comunicación de datos, contacte con proveedor moxxii", "OK");
                }

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());   
            } 
        }
    }
}
