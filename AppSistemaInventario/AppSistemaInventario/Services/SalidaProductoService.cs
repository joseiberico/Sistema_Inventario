using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class SalidaProductoService : IVistaService<SalidaProducto>
    {
        private static readonly HttpClient _Client = new HttpClient();
        private static string _baseurl;

        public SalidaProductoService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        public async Task<List<SalidaProducto>> GetAll()
        {
            var response = await _Client.GetAsync(_baseurl + "SalidaProducto");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }

            List<SalidaProducto> Salidas = JsonConvert.DeserializeObject<List<SalidaProducto>>(content);
            return Salidas;
        }

        public async Task<SalidaProducto> GetById(int id)
        {
            var response = await _Client.GetAsync(_baseurl + "SalidaProducto/" + id);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            var Salida = JsonConvert.DeserializeObject<SalidaProducto>(content);
            return Salida;
        }

        public async Task<SalidaProducto> Create(SalidaProducto entity)
        {
            // Serializar el objeto cliente a JSON
            var jsonSalida = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON del cliente
            var httpContent = new StringContent(jsonSalida, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await _Client.PostAsync(_baseurl + "SalidaProducto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Salida
            var SalidaAgregado = JsonConvert.DeserializeObject<SalidaProducto>(content);

            return SalidaAgregado;
        }
    }
}
