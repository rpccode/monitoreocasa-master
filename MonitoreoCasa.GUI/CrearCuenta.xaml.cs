using MonitoreoCasa.BIZ;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using MonitoreoCasa.DAL.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonitoreoCasa.GUI
{
    /// <summary>
    /// Lógica de interacción para CrearCuenta.xaml
    /// </summary>
    public partial class CrearCuenta : Window
    {
        private IManejadorUsuario manejador;
        public CrearCuenta()
        {
            InitializeComponent();
            manejador = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txbNombre.Text))
            {
                MessageBox.Show("Te falta el nombre", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(pswPass1.Password))
            {
                MessageBox.Show("Te falta la contraseña", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(pswPass2.Password))
            {
                MessageBox.Show("Te falta la segunda contraseña", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (pswPass1.Password != pswPass2.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if(manejador.Agregar(new Usuario()
            {
                NombreUsuario=txbNombre.Text,
                Password=pswPass2.Password
            }) != null)
            {
                MessageBox.Show("Cuenta creada, ya puede iniciar sesión", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear la cuenta...", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
