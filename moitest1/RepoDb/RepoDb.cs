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

namespace moitest1.RepoDb
{
    class RepoDb
    {
        // Client reference.
        private MobileServiceClient client;
        private IMobileServiceSyncTable<Usuario> usuarioTable;
        const string localDbFilename = "moitest1.db";
        
        // URL of the mobile app backend.
        const string applicationURL = @"https://moitest1.azurewebsites.net";


    }
}