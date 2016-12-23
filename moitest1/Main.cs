/*
 * To add Offline Sync Support:
 *  1) Add the NuGet package Microsoft.Azure.Mobile.Client.SQLiteStore (and dependencies) to all client projects
 *  2) Uncomment the #define OFFLINE_SYNC_ENABLED
 *
 * For more information, see: http://go.microsoft.com/fwlink/?LinkId=717898
 */
#define OFFLINE_SYNC_ENABLED

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Content;
using Android.Runtime;

using System;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using moitest1;

#if OFFLINE_SYNC_ENABLED
using Microsoft.WindowsAzure.MobileServices.Sync;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
#endif

namespace moitest1
{
    [Activity(MainLauncher = true, Icon = "@drawable/ic_launcher", Label = "Main",  Theme = "@style/AppTheme")]
    public class Main : Activity
    {
        Button btnUsuarios;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            CurrentPlatform.Init();

            btnUsuarios = FindViewById<Button>(Resource.Id.btnUsuarios);

            btnUsuarios.Click += BtnUsuarios_Click;

        }

        private void BtnUsuarios_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(UsuarioActivity));
        }
    }
}