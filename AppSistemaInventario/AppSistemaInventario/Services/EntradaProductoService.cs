using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class EntradaProductoService: IVistaService<EntradaProducto>
    {
        private static readonly HttpClient _Client = new HttpClient();
        private static string _baseurl;

        public EntradaProductoService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        public async Task<List<EntradaProducto>> GetAll()
        {
            var response = await _Client.GetAsync(_baseurl + "EntradaProducto");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }

            List<EntradaProducto> Entradas = JsonConvert.DeserializeObject<List<EntradaProducto>>(content);
            return Entradas;
        }

        public async Task<EntradaProducto> GetById(int id)
        {
            var response = await _Client.GetAsync(_baseurl + "EntradaProducto/" + id);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            var Entrada = JsonConvert.DeserializeObject<EntradaProducto>(content);
            return Entrada;
        }

        public async Task<EntradaProducto> Create(EntradaProducto entity)
        {
            // Serializar el objeto cliente a JSON
            var jsonEntrada = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON del cliente
            var httpContent = new StringContent(jsonEntrada, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await _Client.PostAsync(_baseurl + "EntradaProducto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Entrada
            var EntradaAgregado = JsonConvert.DeserializeObject<EntradaProducto>(content);

            return EntradaAgregado;
        }

        
    }
}
