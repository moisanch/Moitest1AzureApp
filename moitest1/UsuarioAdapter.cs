using System;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace moitest1
{
    public class UsuarioAdapter : BaseAdapter<Usuario>
    {
        Activity activity;
        int layoutResourceId;
        List<Usuario> items = new List<Usuario>();

        public UsuarioAdapter(Activity activity, int layoutResourceId)
        {
            this.activity = activity;
            this.layoutResourceId = layoutResourceId;
        }

        //Returns the view for a specific item on the list
        public override View GetView(int position, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            var row = convertView;
            var currentItem = this[position];
            TextView etUsuario;
            TextView etPassword;

            if (row == null)
            {
                var inflater = activity.LayoutInflater;
                row = inflater.Inflate(layoutResourceId, parent, false);
            }
            //else checkBox.Text = currentItem.Text;

            etUsuario = row.FindViewById<TextView>(Resource.Id.textRowUsuario);
            etPassword = row.FindViewById<TextView>(Resource.Id.textRowPassword);

            etUsuario.Text = currentItem.User;
            etPassword.Text = currentItem.Pass;


            return row;
        }

        public void Add(Usuario user)
        {
            items.Add(user);
            NotifyDataSetChanged();
        }

        public void Clear()
        {
            items.Clear();
            NotifyDataSetChanged();
        }

        public void Remove(Usuario user)
        {
            items.Remove(user);
            NotifyDataSetChanged();
        }

        #region implemented abstract members of BaseAdapter

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int Count
        {
            get
            {
                return items.Count;
            }
        }

        public override Usuario this[int position]
        {
            get
            {
                return items[position];
            }
        }

        #endregion
    }
}