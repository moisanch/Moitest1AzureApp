using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;

namespace moitest1.MoitestDB
{
    class RepoDb
    {
        public MobileServiceClient client;
        public IMobileServiceSyncTable<Usuario> usuarioTable;
        const string localDbFilename = "moitest1.db";
        const string applicationURL = @"https://moitest1.azurewebsites.net";
        bool isInitialized;

      
        public async Task InitLocalStoreAsync()
        {
            if (isInitialized)
                return;

            client = new MobileServiceClient(applicationURL);

            var store = new MobileServiceSQLiteStore(localDbFilename);
            store.DefineTable<Usuario>();

            // Uses the default conflict handler, which fails on conflict
            // To use a different conflict handler, pass a parameter to InitializeAsync.
            // For more details, see http://go.microsoft.com/fwlink/?LinkId=521416
            await client.SyncContext.InitializeAsync(store);
            usuarioTable = client.GetSyncTable<Usuario>();

            isInitialized = true;
        }

        public async Task SyncAsync(bool pullData = false)
        {
            try
            {
                await client.SyncContext.PushAsync();

                if (pullData)
                {
                    await usuarioTable.PullAsync("allUsuarios", usuarioTable.CreateQuery()); // query ID is used for incremental sync
                }
            }
            catch (Java.Net.MalformedURLException)
            {
                CreateAndShowDialog(new Exception("There was an error creating the Mobile Service. Verify the URL"), "Error");
            }
            catch (Exception e)
            {
                CreateAndShowDialog(e, "Error");
            }
        }




        public void CreateAndShowDialog(Exception exception, String title)
        {
            CreateAndShowDialog(exception.Message, title);
        }

        public void CreateAndShowDialog(string message, string title)
        {
            AlertDialog.Builder builder = new AlertDialog.Builder(Application.Context);

            builder.SetMessage(message);
            builder.SetTitle(title);
            builder.Create().Show();
        }

    }
}