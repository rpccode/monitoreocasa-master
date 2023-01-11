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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonitoreoCasa.GUI
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IManejadorUsuario manejador;
        public MainWindow()
        {
            InitializeComponent();
            manejador = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
        }

        private void btnCrearCuenta_Click(object sender, RoutedEventArgs e)
        {
            CrearCuenta ventana = new CrearCuenta();
            ventana.ShowDialog();
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Usuario u = manejador.Login(txbUsuario.Text, pswPassword.Password);
            if (u != null)
            {
                Monitoreo ventana = new Monitoreo(u);
                ventana.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nombre de usuario o contraseña incorrecto...", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
