using MonitoreoCasa.BIZ;
using MonitoreoCasa.COMMON.Entidades;
using MonitoreoCasa.COMMON.Interfaces;
using MonitoreoCasa.DAL.Local;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
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
    /// Lógica de interacción para Monitoreo.xaml
    /// </summary>
    public partial class Monitoreo : Window
    {
        private IManejadorSensor manejadorSensor;
        private IManejadorMedicion manejadorMedicion;
        private Usuario usuario;
        public Monitoreo(Usuario u)
        {
            InitializeComponent();
            manejadorSensor = new ManejadorSensor(new RepositorioGenerico<Sensor>());
            manejadorMedicion = new ManejadorMedicion(new RepositorioGenerico<Medicion>());
            usuario = u;
            ActualizarListaDeSensores();
        }

        private void ActualizarListaDeSensores()
        {
            lstSensores.ItemsSource = null;
            lstSensores.ItemsSource = manejadorSensor.SensoresDeUsuario(usuario.Id);
        }

        private void btnAgregarSensor_Click(object sender, RoutedEventArgs e)
        {
            AgregarSensor ventana = new AgregarSensor(new Sensor()
            {
                IdUsuario = usuario.Id
            });
            ventana.ShowDialog();
            ActualizarListaDeSensores();
        }

        private void btnEliminarSensor_Click(object sender, RoutedEventArgs e)
        {
            Sensor sensor = lstSensores.SelectedItem as Sensor;
            if (sensor != null)
            {
                if (MessageBox.Show("Realmente deseas eliminar el sensor " + sensor.Tipo + "?", "Monitoreo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (manejadorSensor.Eliminar(sensor.Id))
                    {
                        MessageBox.Show("Sensor eliminado", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ActualizarListaDeSensores();
                    }
                }
            }
        }

        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime inicio = dtpInicio.Value.Value;
                DateTime fin = dtpFin.Value.Value;
                if (inicio > fin)
                {
                    MessageBox.Show("El inicio del intervalo es mayor al final, verifique...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    Sensor sensor = lstSensores.SelectedItem as Sensor;
                    if (sensor != null)
                    {
                        List<Medicion> datos = manejadorMedicion.MedicionesDeSensorEnIntevalo(sensor.Id, inicio, fin).OrderBy(d=>d.FechaHora).ToList();
                        dtgDatos.ItemsSource = datos;
                        PlotModel model = new PlotModel();
                        DateTimeAxis ejeX = new DateTimeAxis();
                        ejeX.Position = AxisPosition.Bottom;

                        LinearAxis ejeY = new LinearAxis();
                        ejeY.Position = AxisPosition.Left;

                        model.Axes.Add(ejeX);
                        model.Axes.Add(ejeY);

                        model.Title="Datos de " + sensor.Nombre;
                        LineSeries linea = new LineSeries();
                        foreach (var item in datos)
                        {
                            linea.Points.Add(new DataPoint(DateTimeAxis.ToDouble(item.FechaHora), item.Valor));
                        }
                        linea.Title = sensor.UnidadDeMedida;
                        model.Series.Add(linea);
                        chrGrafico.Model = model;
                    }
                    else
                    {
                        MessageBox.Show("No se ha seleccionado sensor...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Verifique los campos del intervalo...", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }
    }
}
