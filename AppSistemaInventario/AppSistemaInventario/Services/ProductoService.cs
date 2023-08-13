using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class ProductoService: IGlobalService<Producto>
    {
        private static readonly HttpClient _Client = new HttpClient();
        private static string _baseurl;

        public ProductoService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        public async Task<List<Producto>> GetAll()
        {
            var response = await _Client.GetAsync(_baseurl + "Producto");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }

            List<Producto> productos = JsonConvert.DeserializeObject<List<Producto>>(content);
            return productos;
        }

        public async Task<Producto> GetById(int Id)
        {
            var response = await _Client.GetAsync(_baseurl + "Producto/" + Id);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            var producto = JsonConvert.DeserializeObject<Producto>(content);
            return producto;
        }

        public async Task<Producto> Create(Producto entity)
        {
            // Serializar el objeto cliente a JSON
            var jsonProducto = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON del cliente
            var httpContent = new StringContent(jsonProducto, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await _Client.PostAsync(_baseurl + "Producto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Producto
            var productoAgregado = JsonConvert.DeserializeObject<Producto>(content);

            return productoAgregado;
        }

        public async Task<Producto> Update(Producto entity)
        {
            // Serializar el objeto categoria a JSON
            var jsonProducto = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON de la categoria
            var httpContent = new StringContent(jsonProducto, Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT al servidor
            var response = await _Client.PutAsync(_baseurl + "Producto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Categoria
            var productoActualizado = JsonConvert.DeserializeObject<Producto>(content);

            return productoActualizado;
        }

        public async Task<bool> Delete(int id)
        {
            // Realizar la solicitud DELETE al servidor
            var response = await _Client.DeleteAsync(_baseurl + "Producto/" + id);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return false;
            }

            return true;
        }
    }
}
