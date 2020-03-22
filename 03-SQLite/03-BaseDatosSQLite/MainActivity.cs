using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using System;

namespace _03_BaseDatosSQLite
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            TextView lblID = FindViewById<TextView>(2131230812);
            TextView lblNombre = FindViewById<TextView>(2131230813);

            EditText txtID = FindViewById<EditText>(2131230908);
            EditText txtNombre = FindViewById<EditText>(2131230909);

            Button btnConsultar = FindViewById<Button>(2131230760);
            Button btnInsertar = FindViewById<Button>(2131230762);
            Button btnActualizar = FindViewById<Button>(2131230759);
            Button btnEliminar = FindViewById<Button>(2131230761);

            btnConsultar.Click += (sender, e) =>
            {
                try
                {
                    Persona resultado = null;
                    if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    {
                        resultado = new Auxiliar().Seleccionar(txtNombre.Text.Trim());
                        if (resultado != null)
                        {
                            txtID.Text = resultado.ID.ToString();
                        }
                        else
                        {
                            Toast.MakeText(this, "Registro no encontrado", ToastLength.Long).Show();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

            btnInsertar.Click += (sender, e) =>
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    {
                        new Auxiliar().Guardar(new Persona() { ID = 0, Nombre = txtNombre.Text.Trim() });
                        Toast.MakeText(this, "Registro guardado", ToastLength.Long).Show();
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingrese un nombre", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

            btnActualizar.Click += (sender, e) =>
            {
                int ID;
                try
                {
                    if (!string.IsNullOrEmpty(txtID.Text.Trim()) && !string.IsNullOrEmpty(txtNombre.Text.Trim()))
                    {
                        if (int.TryParse(txtID.Text.Trim(), out ID))
                        {
                            new Auxiliar().Guardar(new Persona() { ID = ID, Nombre = txtNombre.Text });
                            Toast.MakeText(this, "Registro guardado", ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Por favor ingrese los datos correctamente", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingrese los datos correctamente", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

            btnEliminar.Click += (sender, e) =>
            {
                int ID;
                try
                {
                    if (!string.IsNullOrEmpty(txtID.Text.Trim()))
                    {
                        if (int.TryParse(txtID.Text.Trim(), out ID))
                        {
                            new Auxiliar().Eliminar(ID);
                            Toast.MakeText(this, "Registro eliminado", ToastLength.Long).Show();
                        }
                        else
                        {
                            Toast.MakeText(this, "Por favor ingrese los datos correctamente", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingrese los datos correctamente", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
                }
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}