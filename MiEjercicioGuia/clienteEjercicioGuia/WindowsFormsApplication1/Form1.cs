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
            if (radioButton3.Checked) // Solo ejecuta el código si está marcado
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
        }

        private void connect_Click(object sender, EventArgs e)
        {
            try
            {
                // IP del servidor y puerto
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9070);

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
            if (radioButton4.Checked) // Solo ejecuta el código si está marcado
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
        }

        private void Bonito_CheckedChanged(object sender, EventArgs e)
        {
            if (Bonito.Checked) // Solo ejecuta el código si está marcado
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


        }

        private void Longitud_CheckedChanged(object sender, EventArgs e)
        {
            if (Longitud.Checked) // Solo ejecuta el código si está marcado
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

       

        private void buttonAltura_CheckedChanged_1(object sender, EventArgs e)
        {
            if (buttonAltura.Checked) // Solo ejecuta el código si está marcado
            {
                try
                {
                    // Verificar si es alto o bajo
                    string nombreInput = nombre.Text;
                    string alturaInput = altura.Text;  // Asumiendo que hay un TextBox llamado "altura" donde se ingresa la altura

                    // Verificar si se ha ingresado una altura válida
                    if (string.IsNullOrEmpty(alturaInput) || !float.TryParse(alturaInput, out float alturaParsed))
                    {
                        MessageBox.Show("Por favor ingresa una altura válida.");
                        return;
                    }

                    // Formar la solicitud
                    string mensaje = "3/" + nombreInput + "/" + alturaParsed.ToString();
                    byte[] msg = Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    // Recibir la respuesta del servidor
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
        }
    }
}
