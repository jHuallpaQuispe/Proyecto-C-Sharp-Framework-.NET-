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
				comando.CommandText = "Select D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, S.Descripcion SegundoEstilo, T.Descripcion Tipo_Edicion, D.Id Id, IdEstilo, IdSegundoEstilo, IdTipoEdicion from DISCOS D, ESTILOS  E, ESTILOS S, TIPOSEDICION T Where E.Id = D.IdEstilo and T.Id = D.IdTipoEdicion and S.Id = D.IdSegundoEstilo and D.Activo = 1";
				comando.Connection = conexion;

				conexion.Open();
				lector = comando.ExecuteReader();

				//para leer
				while (lector.Read())
				{
					Disco aux = new Disco();
					aux.Id = (int)lector["Id"];
					aux.Titulo = (string)lector["Titulo"];
					aux.FechaLanzamiento = (DateTime)lector["FechaLanzamiento"];
					aux.CantidadCanciones = (int)lector["CantidadCanciones"];

					//Confirmamos si es null o no
					if (!(lector["UrlImagenTapa"] is DBNull))
						aux.Imagen = (string)lector["UrlImagenTapa"];
					
					aux.Estilo = new Estilo();
					aux.Estilo.Id = (int)lector["IdEstilo"];
					aux.Estilo.Descripcion = (string)lector["Estilo"];

					aux.SegundoEstilo = new Estilo();
					aux.SegundoEstilo.Id = (int)lector["IdSegundoEstilo"];
					aux.SegundoEstilo.Descripcion = (string)lector["SegundoEstilo"];

					aux.TipoEdicion = new TipoEdicion();
					aux.TipoEdicion.Id = (int)lector["IdTipoEdicion"];
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
			AccesoDatos dato = new AccesoDatos();
			
			try
			{
				dato.setearConsulta("update DISCOS set Titulo = @titulo, FechaLanzamiento = @fecha, CantidadCanciones = @cantCanciones, UrlImagenTapa = @img, IdEstilo = @idEstilo, IdTipoEdicion = @idTipoEdicion, IdSegundoEstilo = @idSegundoEstilo Where Id = @id");
				dato.Parametro("@titulo",disco.Titulo);
                dato.Parametro("@fecha",disco.FechaLanzamiento);
                dato.Parametro("@cantCanciones",disco.CantidadCanciones);
                dato.Parametro("@img",disco.Imagen);
                dato.Parametro("@idEstilo",disco.Estilo.Id);
                dato.Parametro("@idTipoEdicion",disco.TipoEdicion.Id);
                dato.Parametro("@idSegundoEstilo",disco.SegundoEstilo.Id);
                dato.Parametro("@id",disco.Id);
				
				dato.ejecutarAccion();

            }
            catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				dato.cerrarConexion();
				
			}
		}

		public void eliminarFisico (int id)
		{
			AccesoDatos dato = new AccesoDatos();

			try
			{
                dato.setearConsulta("delete DISCOS where id = @id");
                dato.Parametro("@id", id);

				dato.ejecutarAccion();
            }
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				dato.cerrarConexion();
			}
			
		}

		public void eliminarLogico(int id)
		{
			AccesoDatos dato = new AccesoDatos ();
			try
			{
				dato.setearConsulta("update DISCOS set Activo = 0 where Id = @id");
				dato.Parametro("@id", id);
				dato.ejecutarAccion();
				
			}
			catch (Exception ex)
			{

				throw ex;
			}
			finally
			{
				dato.cerrarConexion() ;
			}
		}

        public List<Disco> filtrar(string campo, string condicion, string busqueda)
        {
			List<Disco> lista = new List<Disco>();
			AccesoDatos datos = new AccesoDatos();
			try
			{
				string consulta = "Select D.Titulo, D.FechaLanzamiento, D.CantidadCanciones, D.UrlImagenTapa, E.Descripcion Estilo, S.Descripcion SegundoEstilo, T.Descripcion Tipo_Edicion, D.Id Id, IdEstilo, IdSegundoEstilo, IdTipoEdicion from DISCOS D, ESTILOS  E, ESTILOS S, TIPOSEDICION T Where E.Id = D.IdEstilo and T.Id = D.IdTipoEdicion and S.Id = D.IdSegundoEstilo and D.Activo = 1 and ";

				switch (campo)
				{
                    case "Título":
                        switch (condicion)
                        {
                            case "Comienza con":
								consulta += "D.Titulo like '" + busqueda + "%'";
                                break;
                            case "Termina con":
                                consulta += "D.Titulo like '%" + busqueda + "'";

                                break;
                            default:
                                //Contiene
                                consulta += "D.Titulo like '%" + busqueda + "%'";

                                break;
                        }
                        break;
					case "Cantidad de Canciones":
						switch (condicion)
						{
							case "Mayor a":

								consulta += "D.CantidadCanciones > "+ busqueda;
								break;
							case "Menor a":
                                consulta += "D.CantidadCanciones < " + busqueda;

                                break;
                            default:
                                consulta += "D.CantidadCanciones =	 " + busqueda;

                                break;
						}
						break;
					

                    case "Estilo":
						//Tambien busca para el segundo estilo.
                        switch (condicion)
                        {
                            case "Comienza con":
								consulta += "(E.Descripcion like '" + busqueda + "%'or S.Descripcion like '" + busqueda + "%')";
                                break;
                            case "Termina con":
                                consulta += "(E.Descripcion like '%" + busqueda + "'or S.Descripcion like '%" + busqueda + "')";

                                break;
                            default:
                                //Contiene
                                consulta += "(E.Descripcion like '%" + busqueda + "%'or S.Descripcion like '%" + busqueda + "%')";
                                break;
                        }
                        break;
                    default:
						//Busca por tipo de edicion
						switch (condicion)
						{
							case "Comienza con":
								consulta += "T.Descripcion like '" + busqueda + "%'";

                                break;
							case "Termina con":
                                consulta += "T.Descripcion like '%" + busqueda + "'";

                                break;
							default:
                                //Contiene
                                consulta += "T.Descripcion like '%" + busqueda + "%'";

                                break;
						}
						break;
                }

				datos.setearConsulta (consulta);
				datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    Disco aux = new Disco();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Titulo = (string)datos.Lector["Titulo"];
                    aux.FechaLanzamiento = (DateTime)datos.Lector["FechaLanzamiento"];
                    aux.CantidadCanciones = (int)datos.Lector["CantidadCanciones"];

                    //Confirmamos si es null o no
                    if (!(datos.Lector["UrlImagenTapa"] is DBNull))
                        aux.Imagen = (string)datos.Lector["UrlImagenTapa"];

                    aux.Estilo = new Estilo();
                    aux.Estilo.Id = (int)datos.Lector["IdEstilo"];
                    aux.Estilo.Descripcion = (string)datos.Lector["Estilo"];

                    aux.SegundoEstilo = new Estilo();
                    aux.SegundoEstilo.Id = (int)datos.Lector["IdSegundoEstilo"];
                    aux.SegundoEstilo.Descripcion = (string)datos.Lector["SegundoEstilo"];

                    aux.TipoEdicion = new TipoEdicion();
                    aux.TipoEdicion.Id = (int)datos.Lector["IdTipoEdicion"];
                    aux.TipoEdicion.Descripcion = (string)datos.Lector["Tipo_Edicion"];


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
				datos.cerrarConexion() ;
			}
        }
    }
}
