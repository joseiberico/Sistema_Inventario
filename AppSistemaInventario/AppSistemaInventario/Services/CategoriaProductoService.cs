using ApiSistemaInventario.Services.iServices;
using AppSistemaInventario.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class CategoriaProductoService : IGlobalService<CategoriaProducto>
    {
        //private static string _usuario;
        //private static string _clave;
        private static readonly HttpClient _Client = new HttpClient();
        private static string _baseurl;

        public CategoriaProductoService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            //_usuario = builder.GetSection("ApiSettings:nombreUsuario").Value;
            //_clave = builder.GetSection("ApiSettings:clave").Value;
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        //public async Task Autenticar()
        //{
        //    var cliente = new HttpClient();
        //    cliente.BaseAddress = new Uri(_baseurl);

        //    var credenciales = new Usuario() { Username = _usuario, Password = _clave };
        //    var content = new StringContent(JsonConvert.SerializeObject(credenciales), Encoding.UTF8, "application/json");
        //    var response = await cliente.PostAsync("Usuario/Autenticar", content);
        //    var json_respuesta = await response.Content.ReadAsStringAsync();
        //    var resultado = JsonConvert.DeserializeObject<ResultadoCredencial>(json_respuesta);
        //    _token = resultado.Token;
        //}
        public async Task<CategoriaProducto> Create(CategoriaProducto entity)
        {
            // Serializar el objeto cliente a JSON
            var jsonUsuario = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON del cliente
            var httpContent = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await _Client.PostAsync(_baseurl + "CategoriaProducto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Usuario
            var CategoriaAgregada = JsonConvert.DeserializeObject<CategoriaProducto>(content);

            return CategoriaAgregada;
        }

        public async Task<bool> Delete(int id)
        {
            // Realizar la solicitud DELETE al servidor
            var response = await _Client.DeleteAsync(_baseurl + "CategoriaProducto/" + id);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return false;
            }

            return true;
        }

        public async Task<List<CategoriaProducto>> GetAll()
        {
            var response = await _Client.GetAsync(_baseurl + "CategoriaProducto");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }

            List<CategoriaProducto> categoriaProductos = JsonConvert.DeserializeObject<List<CategoriaProducto>>(content);
            return categoriaProductos;
        }

        public async Task<CategoriaProducto> GetById(int id)
        {
            var response = await _Client.GetAsync(_baseurl + "CategoriaProducto/" + id);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            var categoriaProducto = JsonConvert.DeserializeObject<CategoriaProducto>(content);
            return categoriaProducto;
        }

        public async Task<CategoriaProducto> Update(CategoriaProducto entity)
        {
            // Serializar el objeto Usuario a JSON
            var jsonUsuario = JsonConvert.SerializeObject(entity);

            // Crear el contenido HTTP con el JSON de la Usuario
            var httpContent = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT al servidor
            var response = await _Client.PutAsync(_baseurl + "CategoriaProducto", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Usuario
            var categoriaProducto = JsonConvert.DeserializeObject<CategoriaProducto>(content);

            return categoriaProducto;
        }
    }
}
