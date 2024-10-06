using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

           
        }

   
        

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Enviar solicitud de verificación de palíndromo
                string mensaje = "4/" + nombre.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido enviar la petición al servidor.");
            }
        }

        private void connect_Click(object sender, EventArgs e)
        {
            try
            {
                // IP del servidor y puerto
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9060);

                // Crear el socket
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Intentar conectar
                server.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado al servidor.");
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido conectar con el servidor.");
            }
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.BackColor = Color.Gray;
                server.Shutdown(SocketShutdown.Both);
                server.Close();
                MessageBox.Show("Desconectado del servidor.");
            }
            catch (Exception)
            {
                MessageBox.Show("No se ha podido desconectar.");
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // Enviar solicitud para convertir el nombre a mayúsculas
                string mensaje = "5/" + nombre.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                MessageBox.Show(mensaje);
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido enviar la petición al servidor.");
            }
        }

        private void Bonito_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                    // Solicitar si el nombre es bonito
                    string mensaje = "2/" + nombre.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    if (mensaje == "SI")
                        MessageBox.Show("Tu nombre ES bonito.");
                    else
                        MessageBox.Show("Tu nombre NO es bonito. Lo siento.");
                
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido enviar la petición al servidor.");
            }


        }

        private void Longitud_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                               
                    // Solicitar la longitud del nombre
                    string mensaje = "1/" + nombre.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                    MessageBox.Show("La longitud de tu nombre es: " + mensaje);
               
            }
            catch (SocketException)
            {
                MessageBox.Show("No he podido enviar la petición al servidor.");
            }


        }
    }
}
