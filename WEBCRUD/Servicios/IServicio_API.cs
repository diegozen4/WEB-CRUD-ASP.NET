using WEBCRUD.Models;

namespace WEBCRUD.Servicios
{
    public interface IServicio_API
    {
        Task<List<Producto>> Lista();
        Task<Producto> Obtener(int idProducto);

        Task<bool> Guardar(Producto objeto);

        Task<bool> Editar(Producto objeto);

        Task<bool> Eliminar(int idProducto);
    }
}
