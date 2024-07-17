using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace business
{
    public class EstiloBusiness
    {
        public List<Estilo> listar()
        {
            List<Estilo> lista = new List<Estilo>();
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.setearConsulta("Select Id, Descripcion From ESTILOS");
                acceso.ejecutarConsulta();

                while (acceso.Lector.Read())
                {
                    Estilo aux = new Estilo();

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
