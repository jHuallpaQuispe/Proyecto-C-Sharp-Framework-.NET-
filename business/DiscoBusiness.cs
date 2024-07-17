using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using dominio;
namespace business
{
    public class DiscoBusiness
    {
        public List<Disco> listar()
        {
			List<Disco> lista = new List<Disco>();
			SqlConnection conexion = new SqlConnection(); // para la conexion
			SqlCommand comando = new SqlCommand(); // para realizar las consultas
			SqlDataReader lector; // donde guardaremos los datos
			try
			{
				conexion.ConnectionString = "server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true";
				comando.CommandType = System.Data.CommandType.Text;
				comando.CommandText = "Select D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, S.Descripcion SegundoEstilo, T.Descripcion Tipo_Edicion, D.Id from DISCOS D, ESTILOS  E, ESTILOS S, TIPOSEDICION T Where E.Id = D.IdEstilo and T.Id = D.IdTipoEdicion and S.Id = D.IdSegundoEstilo";
				comando.Connection = conexion;

				conexion.Open();
				lector = comando.ExecuteReader();

				//para leer
				while (lector.Read())
				{
					Disco aux = new Disco();
					aux.Titulo = (string)lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)lector["CantidadCanciones"];

					//Confirmamos si es null o no
					if (!(lector["UrlImagenTapa"] is DBNull))
						aux.Imagen = (string)lector["UrlImagenTapa"];
					
					aux.Estilo = new Estilo();
					aux.Estilo.Descripcion = (string)lector["Estilo"];

					aux.SegundoEstilo = new Estilo();
					aux.SegundoEstilo.Descripcion = (string)lector["SegundoEstilo"];

					aux.TipoEdicion = new TipoEdicion();
					aux.TipoEdicion.Descripcion = (string)lector["Tipo_Edicion"];
					

					lista.Add(aux);
				}

				conexion.Close();
                return lista;
			}
			catch (Exception ex)
			{

				throw ex;
			}
        }

		public void Agregar(Disco nuevo)
		{
			AccesoDatos dato = new AccesoDatos();

			try
			{
				dato.setearConsulta("insert into DISCOS (Titulo, FechaLanzamiento, CantidadCanciones, IdEstilo, IdTipoEdicion, IdSegundoEstilo, UrlImagenTapa) values (@titulo,@fechaLanzamiento,@cantidadCanciones,@idEstilo,@idTipoEdicion,@idSegundoEstilo, @urlImagenTapa)");
				dato.Parametro("@titulo",nuevo.Titulo);
				dato.Parametro("@fechaLanzamiento",nuevo.FechaLanzamiento);
                dato.Parametro("@cantidadCanciones",nuevo.CantidadCanciones);
				dato.Parametro("@urlImagenTapa", nuevo.Imagen);
				dato.Parametro("@idEstilo",nuevo.Estilo.Id);
                dato.Parametro("@idTipoEdicion",nuevo.TipoEdicion.Id);
                dato.Parametro("@idSegundoEstilo",nuevo.SegundoEstilo.Id);

				dato.ejecutarAccion();


            }
            catch (Exception)
			{

				throw;
			}
			finally
			{
				dato.cerrarConexion();
			}
		}

		public void modificar(Disco disco)
		{
			try
			{

			}
			catch (Exception ex)
			{

				throw ex;
			}
		}
    }
}
