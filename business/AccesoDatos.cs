using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace business
{
    public class AccesoDatos
    {
        /*Para hacer la conexion con la base de datos*/
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return this.lector; }
        }

        /*Constructor*/
        public AccesoDatos()
        {
            this.conexion = new SqlConnection("server=.\\SQLEXPRESS; database=DISCOS_DB; integrated security=true");
            this.comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            this.comando.CommandType = System.Data.CommandType.Text;
            this.comando.CommandText = consulta;


        }
        public void ejecutarConsulta()
        {
            this.comando.Connection = this.conexion;
            try
            {
                this.conexion.Open();
                this.lector = this.comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            this.comando.Connection = this.conexion;
            try
            {
                this.conexion.Open();
                this.comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /*Para hacer un insert por Pararametos, lo apreciaremos dentro de la consulta (Select)*/
        public void Parametro(string nombre, object valor)
        {
            this.comando.Parameters.AddWithValue(nombre,valor);
        }
        public void cerrarConexion()
        {
            //Esto es para cerrar tambien el lector si en caso se abrio.
            if(this.lector != null)
                this.lector.Close();

            this.conexion.Close();

        }
    }
}
