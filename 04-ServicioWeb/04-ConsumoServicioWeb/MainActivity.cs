using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace _04_ConsumoServicioWeb
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            
            SetContentView(Resource.Layout.activity_main);

            Button btnConsultar = FindViewById<Button>(2131230759);
            Button btnInsertar = FindViewById<Button>(2131230760);

            EditText txtID = FindViewById<EditText>(2131230908);
            EditText txtTitulo = FindViewById<EditText>(2131230909);
            EditText txtContenido = FindViewById<EditText>(2131230907);

            string uriServicio = "https://jsonplaceholder.typicode.com/posts";

            btnConsultar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente cliente = new Cliente();
                    if (!string.IsNullOrWhiteSpace(txtID.Text))
                    {
                        int id = 0;
                        if (int.TryParse(txtID.Text.Trim(), out id))
                        {
                            var resultado = await cliente.Get<Entrada>(uriServicio + "/" + txtID.Text);
                            if (cliente.codigoHTTP == 200)
                            {
                                txtTitulo.Text = resultado.title;
                                txtContenido.Text = resultado.body;
                                Toast.MakeText(this, "Consulta realizada con éxito", ToastLength.Long).Show();
                            }
                            else
                            {
                                throw new Exception(cliente.codigoHTTP.ToString());
                            }
                        }
                        else
                        {
                            Toast.MakeText(this, "Por favor ingrese un número entero como ID", ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingrese un ID", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
                }
            };

            btnInsertar.Click += async (sender, e) =>
            {
                try
                {
                    Cliente cliente = new Cliente();
                    Entrada entrada = new Entrada();

                    entrada.title = txtTitulo.Text;
                    entrada.body = txtContenido.Text;

                    if (!string.IsNullOrWhiteSpace(txtTitulo.Text) && !string.IsNullOrWhiteSpace(txtContenido.Text))
                    {
                        var resultado = await cliente.Post<Entrada>(entrada, uriServicio);
                        if (cliente.codigoHTTP == 201)
                        {
                            txtID.Text = resultado.id.ToString();
                            Toast.MakeText(this, "Inserción realizada con éxito", ToastLength.Long).Show();
                        }
                        else
                        {
                            throw new Exception(cliente.codigoHTTP.ToString());
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Por favor ingrese el título y el contenido de la entrada", ToastLength.Long).Show();
                    }
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Error: " + ex.Message, ToastLength.Long).Show();
                }
            };

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class Entrada
    {
        public Entrada()
        {
            userId = 1;
            id = 0;
            title = "";
            body = "";
        }

        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

    public class Cliente
    {
        public Cliente()
        {
            codigoHTTP = 200;
        }

        public int codigoHTTP { get; set; }

        //GET
        public async Task<T> Get<T>(string url)
        {
            HttpClient cliente = new HttpClient();
            var response = await cliente.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            codigoHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }

        //POST
        public async Task<T> Post<T>(Entrada item, string url)
        {
            HttpClient cliente = new HttpClient();
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            response = await cliente.PostAsync(url, content);
            json = await response.Content.ReadAsStringAsync();
            codigoHTTP = (int)response.StatusCode;
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}