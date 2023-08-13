using AppSistemaInventario.Models;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class Servicio_API
    {
        private static string _usuario;
        private static string _clave;
        private static string _baseurl;
        private static string _token;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _usuario = builder.GetSection("ApiSettings:nombreUsuario").Value;
            _clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        public async Task Autenticar()
        {
            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseurl);

            var credenciales = new Usuario() { Username = _usuario, Password = _clave };
            var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
            var response = await cliente.PostAsync("Usuario/Autenticar", content);
            var json_respuesta = await response.Content.ReadAsStringAsync();
            var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);
            _token = resultado.Token;
        }
    }
    
}
