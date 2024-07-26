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
using business;
namespace App_Discos_2024
{
    public partial class frmDiscos : Form
    {
        private List<Disco> listaDisco;
        public frmDiscos()
        {
            InitializeComponent();
        }

        private void frmDiscos_Load(object sender, EventArgs e)
        {
            cargar();

            cboCampo.Items.Add("Título");
            cboCampo.Items.Add("Cantidad de Canciones");
            cboCampo.Items.Add("Estilo");
            cboCampo.Items.Add("Tipo de Edición");



        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            //Nos daba un error de SelectedIndexChanged

            if (dgvDiscos.CurrentRow != null)
            {
                Disco seleccionado=(Disco)dgvDiscos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.Imagen);

            }

        }

        private void cargar()
        {
            //Se cargan los datos, lo ponemos en una funcion porque lo vamos usar varias veces
            DiscoBusiness business = new DiscoBusiness();
            try
            {
                listaDisco = business.listar();
                dgvDiscos.DataSource = listaDisco;

                quitarVisibilidad();

                cargarImagen(listaDisco[0].Imagen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void quitarVisibilidad()
        {
            dgvDiscos.Columns["Imagen"].Visible = false;
            dgvDiscos.Columns["Id"].Visible = false;
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaDisco frmAltaDisco = new frmAltaDisco();
            frmAltaDisco.ShowDialog();

            //Cuando se cierra la pantalla abierta,actualizaremos la carga
            cargar();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Disco seleccionado;
            seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;
            frmAltaDisco frmAltaDisco = new frmAltaDisco(seleccionado);
            frmAltaDisco.ShowDialog();

            //Cuando se cierra la pantalla abierta,actualizaremos la carga
            cargar();
        }

        private void btnEliminarFisico_Click(object sender, EventArgs e)
        {
            eliminar();
        }


        private void btnEliminarLogico_Click(object sender, EventArgs e)
        {
            eliminar(true);
        }
        private void eliminar(bool logico = false)
        {
            //Encapsulamos el codigo porque se repetiria en los dos evente de eliminar
            DiscoBusiness business = new DiscoBusiness();
            Disco seleccionado;

            try
            {
                DialogResult resultado = MessageBox.Show("¿De verdad quieres eliminarlo?", "¡Cuidado!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    seleccionado = (Disco)dgvDiscos.CurrentRow.DataBoundItem;

                    //Si es true estamos en una eliminacion logica y si es FALSE estamos en una eliminacion Fisica
                    if (logico)
                        business.eliminarLogico(seleccionado.Id);
                    else
                        business.eliminarFisico(seleccionado.Id);

                    cargar(); // actualizamos la lista
                }

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DiscoBusiness business = new DiscoBusiness();
            try
            {
                if (cboCampo.SelectedItem == null)
                {
                    MessageBox.Show("Selecciona el campo","Advertencia",MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
                else
                {
                    string campo = cboCampo.SelectedItem.ToString();
                    string condicion = cboCondicion.SelectedItem.ToString();
                    string busqueda = txtbusqueda.Text;
                    dgvDiscos.DataSource = business.filtrar(campo, condicion, busqueda);
                }


            }
            catch (Exception ex )
            {

                MessageBox.Show(ex.ToString());
            }

        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Disco> listaFiltrada;
            string filtro = txtFiltro.Text;

            //Para que filtre a partir de n letras
            if (filtro.Length >= 2)
            {

                listaFiltrada = listaDisco.FindAll(x => x.Titulo.ToLower().Contains(txtFiltro.Text.ToLower()) ||
                                                   x.Estilo.ToString().ToLower().Contains(txtFiltro.Text.ToLower()) ||
                                                   x.SegundoEstilo.ToString().ToLower().Contains(txtFiltro.Text.ToLower()) ||
                                                   x.TipoEdicion.ToString().ToLower().Contains(txtFiltro.Text.ToLower()));

            }
            else
            {
                listaFiltrada = listaDisco;
            }

            dgvDiscos.DataSource = null; // Esto limpia la lista
            dgvDiscos.DataSource = listaFiltrada;
            quitarVisibilidad();
        }

        private void cboCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiscoBusiness business = new DiscoBusiness();

            string campo = cboCampo.Text;

            if(campo == "Cantidad de Canciones")
            {
                cboCondicion.Items.Clear();
                cboCondicion.Items.Add("Mayor a");
                cboCondicion.Items.Add("Menor a");
                cboCondicion.Items.Add("Igual a");

            }
            else
            {
                cboCondicion.Items.Clear();
                cboCondicion.Items.Add("Comienza con");
                cboCondicion.Items.Add("Termina con");
                cboCondicion.Items.Add("Contiene");

            }
            cboCondicion.SelectedIndex = 0;


        }
    }
}
