using AppSistemaInventario.Models;
using AppSistemaInventario.Services.IServices;
using Newtonsoft.Json;
using System.Text;

namespace AppSistemaInventario.Services
{
    public class UsuarioService : IUsuarioService<Usuario>
    {
        private static readonly HttpClient _Client = new HttpClient();
        private static string _baseurl;

        public UsuarioService()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseurl = builder.GetSection("ApiSettings:baseurl").Value;
        }

        public async Task<List<Usuario>> GetAll()
        {
            var response = await _Client.GetAsync(_baseurl + "Usuario");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
            }

            List<Usuario> usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);
            return usuarios;
        }

        public async Task<Usuario> GetById(int Id)
        {
            var response = await _Client.GetAsync(_baseurl + "Usuario/" + Id);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            var Usuario = JsonConvert.DeserializeObject<Usuario>(content);
            return Usuario;
        }

        public async Task<Usuario> Create(Usuario Usuario)
        {
            // Serializar el objeto cliente a JSON
            var jsonUsuario = JsonConvert.SerializeObject(Usuario);

            // Crear el contenido HTTP con el JSON del cliente
            var httpContent = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

            // Realizar la solicitud POST al servidor
            var response = await _Client.PostAsync(_baseurl + "Usuario", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Usuario
            var UsuarioAgregado = JsonConvert.DeserializeObject<Usuario>(content);

            return UsuarioAgregado;
        }

        public async Task<Usuario> Update(Usuario Usuario)
        {
            // Serializar el objeto Usuario a JSON
            var jsonUsuario = JsonConvert.SerializeObject(Usuario);

            // Crear el contenido HTTP con el JSON de la Usuario
            var httpContent = new StringContent(jsonUsuario, Encoding.UTF8, "application/json");

            // Realizar la solicitud PUT al servidor
            var response = await _Client.PutAsync(_baseurl + "Usuario", httpContent);

            // Leer la respuesta del servidor
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(content);
                return null;
            }

            // Deserializar la respuesta JSON en un objeto Usuario
            var UsuarioActualizado = JsonConvert.DeserializeObject<Usuario>(content);

            return UsuarioActualizado;
        }

        public async Task<bool> Delete(int Id)
        {
            // Realizar la solicitud DELETE al servidor
            var response = await _Client.DeleteAsync(_baseurl + "Usuario/" + Id);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                return false;
            }

            return true;
        }

        public async Task<Usuario> IniciarSession(string username, string password)
        {
            var loginRequest = new { nombreUsuario = username, clave = password };
            string jsonUser = JsonConvert.SerializeObject(loginRequest);
            HttpContent httpContent = new StringContent(jsonUser, Encoding.UTF8, "application/json");

            var response = await _Client.PostAsync(_baseurl + "Usuario/" + "Autenticar", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Cannot log in user");
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Usuario>(content);
        }

    }
}
