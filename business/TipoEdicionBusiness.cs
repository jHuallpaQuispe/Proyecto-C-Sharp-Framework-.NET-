using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace business
{
    public class TipoEdicionBusiness
    {
        public List<TipoEdicion> listar()
        {
			List<TipoEdicion> lista = new List<TipoEdicion>();
			AccesoDatos acceso = new AccesoDatos();

			try
			{
				acceso.setearConsulta("Select Id, Descripcion From TIPOSEDICION");
				acceso.ejecutarConsulta();

				while (acceso.Lector.Read())
				{
					TipoEdicion aux = new TipoEdicion();
					aux.Id = (int)acceso.Lector["Id"];
					aux.Descripcion = (string)acceso.Lector["Descripcion"];

					lista.Add(aux);
				}
				return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				acceso.cerrarConexion();
			}
        }
    }
}
