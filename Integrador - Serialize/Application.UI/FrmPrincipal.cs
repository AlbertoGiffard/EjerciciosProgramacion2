﻿using System;
using System.Windows.Forms;
using Application.Repositories;
using Application.Helpers;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class FrmPrincipal : Form
    {
        CustomerRepository customerRepository;
        public FrmPrincipal()
        {
            InitializeComponent();
            this.customerRepository = new CustomerRepository();

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmPrincipal_FormClosing(object sender, FormClosingEventArgs e)//SE VA
        {

        }

        private void visualizerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCustomerVisualizer frm = new FrmCustomerVisualizer(this.customerRepository);

            frm.StartPosition = FormStartPosition.CenterScreen;

            frm.Show();
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog open = new OpenFileDialog())
                {
                    open.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        //Obtiene la ruta
                        string path = open.FileName;

                        try
                        {
                            if (Path.GetExtension(path) == ".xml")
                            {                                
                                customerRepository.LoadFromFile(path);
                                MessageBox.Show("Cargado correctamente");
                            }
                            else
                            {
                                MessageBox.Show("Error, extensión no válida");
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show("Error: " + exception.Message);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se puede abrir: {ex.Message}", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogException(ex.Message);
            }
        }
        private void cargarArchivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.customerRepository.SaveToFile(customerRepository.GetAll()))
                {
                    MessageBox.Show("Clientes serializados exitosamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"No se puede Guardar", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"No se puede Guardar: {ex.Message}", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.LogException(ex.Message);
            }
        }

        private void FrmPrincipal_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Deseas Salir?", "test", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                this.Activate();
            }
        }
    }
}
