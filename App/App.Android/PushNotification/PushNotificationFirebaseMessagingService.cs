using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App.Droid.Services.PushNotification
{
    [Service(Exported = true)]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class PushNotificationFirebaseMessagingService : FirebaseMessagingService
    {

        const string TAG = "MyFirebaseMsgService";
        AndroidNotificationManager androidNotification = new AndroidNotificationManager();
        public override void OnMessageReceived(RemoteMessage message)
        {

            Log.Debug(TAG, "From: " + message.From);
            Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            androidNotification.CrearNotificacionLocal(message.GetNotification().Title, message.GetNotification().Body);
            //androidNotification.CrearNotificacionLocal2(message);

        }
        public override void OnNewToken(string token)
        {
            base.OnNewToken(token);

            Preferences.Set("TokenFirebase", token);
            sedRegisterToken(token);
        }
        public void sedRegisterToken(string token)
        {
            //Tu código para registrar el token a tu servidor y base de datos
        }
    }
}