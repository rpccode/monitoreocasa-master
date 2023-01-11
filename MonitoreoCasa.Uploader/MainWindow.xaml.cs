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
using System.Windows.Threading;

namespace MonitoreoCasa.Uploader
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IManejadorUsuario manejadorUsuario;
        IManejadorSensor manejadorSensor;
        IManejadorMedicion manejadorMedicion;
        List<Sensor> sensores;
        CtrlPuerto puertoSerie;
        static StringBuilder log;
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
            manejadorSensor = new ManejadorSensor(new RepositorioGenerico<Sensor>());
            manejadorUsuario = new ManejadorUsuario(new RepositorioGenerico<Usuario>());
            manejadorMedicion = new ManejadorMedicion(new RepositorioGenerico<Medicion>());
            puertoSerie = new CtrlPuerto();
            puertoSerie.DatoObtenido += PuertoSerie_DatoObtenido;
            puertoSerie.Evento += PuertoSerie_Evento;
            cmbPuerto.ItemsSource = puertoSerie.PuertosDisponibles();
            log = new StringBuilder();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            txbLog.Text = log.ToString();
            txbLog.ScrollToEnd();
        }

        private void PuertoSerie_Evento(object sender, EventArgs e)
        {
            IngresarLog("Evento", sender.ToString());
        }

        private void IngresarLog(string origen, string texto)
        {
            log.AppendLine(string.Format("[{0}]: {1}", origen, texto));
        }

        private void PuertoSerie_DatoObtenido(object sender, string e)
        {
            IngresarLog("Hardware", e);
            CargarDato(e);
        }

        private void CargarDato(string e)
        {
            //T=26.00
            string comando = e.Substring(0,1);
            Sensor tipo = sensores.Where(s => s.Comando == comando).SingleOrDefault();
            if (tipo != null)
            {
                if (tipo.EsEvento)
                {
                    manejadorMedicion.Agregar(new Medicion()
                    {
                        IdSensor = tipo.Id,
                        Lugar = tipo.Nombre,
                        Valor = 1
                    });
                }
                else
                {
                    manejadorMedicion.Agregar(new Medicion()
                    {
                        IdSensor = tipo.Id,
                        Lugar = tipo.Nombre,
                        Valor = float.Parse(e.Substring(2, e.Length - 3))
                    });
                }
            }
            else
            {
                IngresarLog("Error", string.Format("No hay comando registrado para [{0}]", comando));
            }
        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            if (cmbPuerto.SelectedItem != null)
            {
                if(!puertoSerie.Conectar(cmbPuerto.SelectedItem as string))
                {
                    MessageBox.Show("Error al conectarme", "Uploader", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnEnviarComando_Click(object sender, RoutedEventArgs e)
        {
            Sensor sensor = cmbComando.SelectedItem as Sensor;
            if (sensor != null)
            {
                puertoSerie.Escribir(sensor.Comando);
            }
        }

        private void btnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            Usuario usuario = manejadorUsuario.Login(txbUsuario.Text, pswPassword.Password);
            if (usuario != null)
            {
                MessageBox.Show("Bienvenido", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Information);
                sensores = manejadorSensor.SensoresDeUsuario(usuario.Id);
                cmbComando.ItemsSource = sensores.Where(s => !s.EsEvento);
            }
            else
            {
                MessageBox.Show("Nombre de Usuario o contraseña incorrectos", "Monitoreo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
