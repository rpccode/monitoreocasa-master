using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoreoCasa.Uploader
{
    public class CtrlPuerto
    {
        SerialPort puerto;
        public string Puerto { get; set; }
        public bool Conectado { get; set; }

        public event EventHandler Evento;
        public event EventHandler<string> DatoObtenido;

        public CtrlPuerto()
        {
            Puerto = "";
            Conectado = false;
        }

        public List<string> PuertosDisponibles()
        {
            return SerialPort.GetPortNames().ToList();
        }

        public bool Conectar(string direccion)
        {
            try
            {
                puerto = new SerialPort();
                puerto.PortName = direccion;
                puerto.Parity = Parity.None;
                puerto.BaudRate = 9600;
                puerto.StopBits = StopBits.One;
                puerto.ReadTimeout = 5000;
                puerto.WriteTimeout = 5000;
                puerto.Handshake = Handshake.None;
                puerto.ReadBufferSize = 64;
                puerto.WriteBufferSize = 64;
                puerto.DtrEnable = true;
                puerto.RtsEnable = true;
                puerto.Open();
                Puerto = direccion;
                Conectado = true;
                Evento("Puerto conectado en " + direccion, null);
                puerto.ReadExisting();
                puerto.DataReceived += Puerto_DataReceived;
                return true;
            }
            catch (Exception ex)
            {
                Puerto = "";
                Conectado = false;
                Evento("Error: " + ex.Message, null);
                return false;
            }
        }

        public bool Desconectar()
        {
            try
            {
                puerto.Close();
                Conectado = false;
                Puerto = "";
                puerto.DataReceived -= Puerto_DataReceived;
                Evento("Puerto Cerrado", null);
                return true;
            }
            catch (Exception ex)
            {
                Evento("Error: " + ex.Message, null);
                return false;
            }
        }

        public bool Escribir(string comando)
        {
            try
            {
                if (Conectado)
                {
                    puerto.WriteLine(comando);
                    Evento("Comando enviado: " + comando, null);
                    return true;
                }
                else
                {
                    Evento("Error: puerto desconectado", null);
                    return false;
                }
            }
            catch (Exception ex)
            {
                Evento("Error: " + ex.Message, null);
                return false;
            }
            
        }

        private void Puerto_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string texto= puerto.ReadLine();
            DatoObtenido(this,texto);
        }
    }
}

