using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.IO;

namespace _02_Archivos
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

            TextView lblGuardar = FindViewById<TextView>(2131230811);
            EditText txtGuardar = FindViewById<EditText>(2131230907);
            TextView lblGuardado = FindViewById<TextView>(2131230810);
            EditText txtGuardado = FindViewById<EditText>(2131230906);
            Button btnGuardar = FindViewById<Button>(2131230760);
            Button btnGuardado = FindViewById<Button>(2131230759);

            btnGuardar.Click += async (sender, e) =>
            {
                string nombreArchivo = "prueba.txt";
                string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string rutaCompleta = Path.Combine(ruta, nombreArchivo);

                using (var escritor = File.CreateText(rutaCompleta))
                {
                    await escritor.WriteLineAsync(txtGuardar.Text);
                }

            };

            btnGuardado.Click += async (sender, e) =>
            {
                string nombreArchivo = "prueba.txt";
                string ruta = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string rutaCompleta = Path.Combine(ruta, nombreArchivo);

                if(File.Exists(rutaCompleta))
                {
                    using (var lector = new StreamReader(rutaCompleta, true))
                    {
                        string textoLeido;
                        while((textoLeido = await lector.ReadLineAsync()) != null)
                        {
                            txtGuardado.Text = textoLeido;
                        }
                    }
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