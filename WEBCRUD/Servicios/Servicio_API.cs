using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEBCRUD.Models;

namespace WEBCRUD.Servicios
{
    public class Servicio_API : IServicio_API
    {
        private readonly string _baseUrl;

        public Servicio_API()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            _baseUrl = builder.GetSection("ApiSetting:baseUrl").Value;
        }

        public async Task<List<Producto>> Lista()
        {
            List<Producto> lista = new List<Producto>();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_baseUrl);

                var response = await cliente.GetAsync("api/Producto/Lista");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<dynamic>(json_respuesta);

                    // Deserializar directamente la lista de productos desde la propiedad 'response'
                    lista = JsonConvert.DeserializeObject<List<Producto>>(resultado.response.ToString());
                }
            }

            return lista;
        }

        public async Task<Producto> Obtener(int idProducto)
        {
            Producto objeto = new Producto();

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_baseUrl);

                var response = await cliente.GetAsync($"api/Producto/Obtener/{idProducto}");

                if (response.IsSuccessStatusCode)
                {
                    var json_respuesta = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<dynamic>(json_respuesta);

                    // Deserializar directamente el objeto Producto desde la propiedad 'response'
                    objeto = JsonConvert.DeserializeObject<Producto>(resultado.response.ToString());
                }
            }

            return objeto;
        }

        public async Task<bool> Guardar(Producto objeto)
        {
            bool respuesta = false;

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_baseUrl);

                var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

                var response = await cliente.PostAsync("api/Producto/Guardar/", content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        public async Task<bool> Editar(Producto objeto)
        {
            bool respuesta = false;

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_baseUrl);

                var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

                var response = await cliente.PutAsync("api/Producto/Editar/", content);

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }

        public async Task<bool> Eliminar(int idProducto)
        {
            bool respuesta = false;

            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri(_baseUrl);

                var response = await cliente.DeleteAsync($"api/Producto/Eliminar/{idProducto}");

                if (response.IsSuccessStatusCode)
                {
                    respuesta = true;
                }
            }

            return respuesta;
        }
    }
}
