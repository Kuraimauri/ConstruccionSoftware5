using System.Text;
using Newtonsoft.Json;

namespace _04_ConsumoServicioWeb;

public partial class MainPage : ContentPage
{
    private const string UriServicio = "https://jsonplaceholder.typicode.com/posts";

    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnConsultarClicked(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                await DisplayAlert("Aviso", "Por favor ingrese un ID", "OK");
                return;
            }

            if (!int.TryParse(txtID.Text.Trim(), out _))
            {
                await DisplayAlert("Aviso", "Por favor ingrese un número entero como ID", "OK");
                return;
            }

            var cliente = new Cliente();
            var resultado = await cliente.Get<Entrada>(UriServicio + "/" + txtID.Text.Trim());
            if (cliente.codigoHTTP == 200 && resultado != null)
            {
                txtTitulo.Text = resultado.title;
                txtContenido.Text = resultado.body;
                await DisplayAlert("Consulta", "Consulta realizada con éxito", "OK");
            }
            else
            {
                throw new Exception(cliente.codigoHTTP.ToString());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error: " + ex.Message, "OK");
        }
    }

    private async void OnInsertarClicked(object? sender, EventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(txtTitulo.Text) || string.IsNullOrWhiteSpace(txtContenido.Text))
            {
                await DisplayAlert("Aviso", "Por favor ingrese el título y el contenido de la entrada", "OK");
                return;
            }

            var cliente = new Cliente();
            var entrada = new Entrada { title = txtTitulo.Text, body = txtContenido.Text };
            var resultado = await cliente.Post<Entrada>(entrada, UriServicio);
            if (cliente.codigoHTTP == 201 && resultado != null)
            {
                txtID.Text = resultado.id.ToString();
                await DisplayAlert("Inserción", "Inserción realizada con éxito", "OK");
            }
            else
            {
                throw new Exception(cliente.codigoHTTP.ToString());
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Error: " + ex.Message, "OK");
        }
    }
}

public class Entrada
{
    public int userId { get; set; } = 1;
    public int id { get; set; } = 0;
    public string title { get; set; } = "";
    public string body { get; set; } = "";
}

public class Cliente
{
    public int codigoHTTP { get; set; } = 200;

    public async Task<T?> Get<T>(string url)
    {
        using var cliente = new HttpClient();
        var response = await cliente.GetAsync(url);
        var json = await response.Content.ReadAsStringAsync();
        codigoHTTP = (int)response.StatusCode;
        return JsonConvert.DeserializeObject<T>(json);
    }

    public async Task<T?> Post<T>(Entrada item, string url)
    {
        using var cliente = new HttpClient();
        var json = JsonConvert.SerializeObject(item);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await cliente.PostAsync(url, content);
        json = await response.Content.ReadAsStringAsync();
        codigoHTTP = (int)response.StatusCode;
        return JsonConvert.DeserializeObject<T>(json);
    }
}
