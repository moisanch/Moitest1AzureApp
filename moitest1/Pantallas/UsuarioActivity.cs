using System;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using moitest1.MoitestDB;


namespace moitest1
{
    [Activity(MainLauncher = false,Icon = "@drawable/ic_launcher", Label = "@string/app_name",Theme = "@style/AppTheme")]
    public class UsuarioActivity : Activity
    {
        private UsuarioAdapter adapter;
        private EditText etUsuario;
        private EditText etPassword;
        private RepoDb db;

        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Usuario);

            etUsuario = FindViewById<EditText>(Resource.Id.edtUsuario);
            etPassword = FindViewById<EditText>(Resource.Id.edtPassword);

            adapter = new UsuarioAdapter(this, Resource.Layout.Row_Usuario);
            var listViewUsers = FindViewById<ListView>(Resource.Id.lstUsuarios);
            listViewUsers.Adapter = adapter;

            db = new RepoDb();
            await db.InitLocalStoreAsync();

            // Load the items from the mobile app backend.
            OnRefreshItemsSelected();
        }

        //Initializes the activity menu
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.activity_users, menu);
            return true;
        }

        //Select an option from the menu
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.menu_refresh_users)
            {
                item.SetEnabled(false);

                OnRefreshItemsSelected();

                item.SetEnabled(true);
            }
            return true;
        }

        // Called when the refresh menu option is selected.
        private async void OnRefreshItemsSelected()
        {
            // refresh view using local store.
            await RefreshItemsFromTableAsync();
        }

        //Refresh the list with the users in the local store.
        private async Task RefreshItemsFromTableAsync()
        {
            try
            {
                // Get the items that weren't marked as completed and add them in the adapter
                var list = await db.usuarioTable.Where(item => item.Deleted == false).ToListAsync();

                adapter.Clear();

                foreach (Usuario current in list)
                    adapter.Add(current);

            }
            catch (Exception e)
            {
                db.CreateAndShowDialog(e, "Error");
            }
        }

        [Java.Interop.Export()]
        public async void AddItem(View view)
        {
            if (db.client == null || string.IsNullOrWhiteSpace(etUsuario.Text) || string.IsNullOrWhiteSpace(etUsuario.Text) )
            {
                return;
            }

            // Create a new user
            var user = new Usuario
            {
                User = etUsuario.Text,
                Pass = etPassword.Text
            };

            try
            {
                // Insert the new item into the local store.
                await db.usuarioTable.InsertAsync(user);

				await db.SyncAsync();

                if (!user.Deleted)
                {
                    adapter.Add(user);
                }
            }
            catch (Exception e)
            {
                db.CreateAndShowDialog(e, "Error");
            }

            etUsuario.Text = "";
            etPassword.Text = "";
            OnRefreshItemsSelected();
        }

    }
}