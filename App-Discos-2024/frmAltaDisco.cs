using business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
namespace App_Discos_2024
{
    public partial class frmAltaDisco : Form
    {
        private Disco disco = null;
        public frmAltaDisco()
        {
            InitializeComponent();
        }
        public frmAltaDisco(Disco disco)
        {
            //si se usa este contructor, es porque estamos modificando
            InitializeComponent();
            this.disco = disco;
            Text = "Modificar Disco";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            DiscoBusiness business = new DiscoBusiness();

            try
            {
                //Verificamos si se modifica o no
                if(this.disco == null)
                    this.disco=new Disco();

                this.disco.Titulo = txtTitulo.Text;
                this.disco.FechaLanzamiento = dtpLanzamiento.Value;
                this.disco.CantidadCanciones = int.Parse(txtCantCanciones.Text);
                this.disco.Imagen = txtUrlImagen.Text;
                this.disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                this.disco.SegundoEstilo = (Estilo)cboSegundoEstilo.SelectedItem;
                this.disco.TipoEdicion = (TipoEdicion)cboTipoEdicion.SelectedItem;

                if(this.disco.Id != 0)
                {
                    business.modificar(this.disco);
                    MessageBox.Show("Modificado Exitosamente..");
                }
                else
                {
                    business.Agregar(this.disco);
                    MessageBox.Show("Se Agrego Exitosamente..");
                }
                


                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            } 


        }

        private void frmAltaDisco_Load(object sender, EventArgs e)
        {
            EstiloBusiness estilos = new EstiloBusiness();
            TipoEdicionBusiness TipoEdicion = new TipoEdicionBusiness();

            try
            {
                cboEstilo.DataSource = estilos.listar();
                cboEstilo.ValueMember = "Id";
                cboEstilo.DisplayMember = "Descripcion";

                cboSegundoEstilo.DataSource = estilos.listar();
                cboSegundoEstilo.ValueMember = "Id";
                cboSegundoEstilo.DisplayMember= "Descripcion";

                cboTipoEdicion.DataSource = TipoEdicion.listar();
                cboTipoEdicion.ValueMember = "Id";
                cboTipoEdicion.DisplayMember = "Descripcion";


                //Verificamos si es para modificar o para agregar 
                if(this.disco != null)
                {
                    txtTitulo.Text = this.disco.Titulo;
                    dtpLanzamiento.Value = this.disco.FechaLanzamiento;
                    txtCantCanciones.Text = this.disco.CantidadCanciones.ToString();
                    txtUrlImagen.Text = this.disco.Imagen;
                    cargarImagen(this.disco.Imagen);

                    cboEstilo.SelectedValue = this.disco.Estilo.Id;
                    cboSegundoEstilo.SelectedValue = this.disco.SegundoEstilo.Id;
                    cboTipoEdicion.SelectedValue = this.disco.TipoEdicion.Id;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }

        private void cargarImagen(string imagen)
        {
            try
            {
                pbxDisco.Load(imagen);

            }
            catch (Exception)
            {

                pbxDisco.Load("https://t3.ftcdn.net/jpg/02/48/42/64/360_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg");
            }
        }
    }
}
