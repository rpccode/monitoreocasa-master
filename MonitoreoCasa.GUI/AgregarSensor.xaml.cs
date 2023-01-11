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
    /// Lógica de interacción para AgregarSensor.xaml
    /// </summary>
    public partial class AgregarSensor : Window
    {
        private IManejadorSensor manejador;
        public AgregarSensor(Sensor sensor)
        {
            InitializeComponent();
            manejador = new ManejadorSensor(new RepositorioGenerico<Sensor>());
            Contenedor.DataContext = sensor;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if(manejador.Agregar(Contenedor.DataContext as Sensor) != null)
            {
                MessageBox.Show("Sensor correctamente creado", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al crear el sensor", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
