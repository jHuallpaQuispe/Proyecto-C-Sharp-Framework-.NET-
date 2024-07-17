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
            //Disco disco = new Disco();

            try
            {
                if(this.disco == null)
                    this.disco=new Disco();

                this.disco.Titulo = txtTitulo.Text;
                this.disco.FechaLanzamiento = dtpLanzamiento.Value;
                this.disco.CantidadCanciones = int.Parse(txtCantCanciones.Text);
                this.disco.Imagen = txtUrlImagen.Text;
                this.disco.Estilo = (Estilo)cboEstilo.SelectedItem;
                this.disco.SegundoEstilo = (Estilo)cboSegundoEstilo.SelectedItem;
                this.disco.TipoEdicion = (TipoEdicion)cboTipoEdicion.SelectedItem;

                business.Agregar(this.disco);
                MessageBox.Show("Se Agrego Exitosamente..");

                business.modificar(this.disco);
                MessageBox.Show("Modificado Exitosamente..");

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

            cboEstilo.DataSource = estilos.listar();
            cboSegundoEstilo.DataSource = estilos.listar();
            cboTipoEdicion.DataSource = TipoEdicion.listar();
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
