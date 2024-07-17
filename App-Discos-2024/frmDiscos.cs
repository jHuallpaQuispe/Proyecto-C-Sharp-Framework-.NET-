﻿using System;
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
            
        }

        private void dgvDiscos_SelectionChanged(object sender, EventArgs e)
        {
            Disco seleccionado=(Disco)dgvDiscos.CurrentRow.DataBoundItem;

            cargarImagen(seleccionado.Imagen);
        }

        private void cargar()
        {
            //Se cargan los datos, lo ponemos en una funcion porque lo vamos usar varias veces
            DiscoBusiness business = new DiscoBusiness();
            try
            {
                listaDisco = business.listar();
                dgvDiscos.DataSource = listaDisco;

                dgvDiscos.Columns["Imagen"].Visible = false;
                cargarImagen(listaDisco[0].Imagen);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
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
    }
}