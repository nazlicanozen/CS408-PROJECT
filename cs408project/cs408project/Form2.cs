using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cs408project
{
    public partial class Form2 : Form
    {

        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<String> usernames = new List<String>();
        List<String> IFusernames = new List<String>();
        List<String> SPSusernames = new List<String>();


        bool terminating = false;
        bool listening = false;

        public Form2()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            //this.FormClosing += new FormClosingEventHandler(Form2_FormClosing);
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ListenButton_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(ListenBox.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);
                serverSocket.Listen(5);

                listening = true;
                ListenButton.Enabled = false;
                ListenBox.Enabled = false;
                
                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }


        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();
                    clientSockets.Add(newClient);
                    logs.AppendText("New TCP Socket opened\n");

                    Thread receiveThread = new Thread(() => Receive(newClient)); // updated
                    receiveThread.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive(Socket thisClient) // updated
        {
            bool connected = true;
            bool sentUsername = false;

            while (connected && !terminating)
            {
                try
                {
                    if(!sentUsername)
                    {
                        Byte[] UsernameBuffer = new Byte[256];
                        thisClient.Receive(UsernameBuffer);
                        string incomingUsername = Encoding.Default.GetString(UsernameBuffer);
                        incomingUsername = incomingUsername.Substring(0, incomingUsername.IndexOf("\0"));

                        if (usernames.Contains(incomingUsername))
                        {
                            connected = false;

                            string confirmMessage = "NO";
                            Byte[] confirmBuffer = Encoding.Default.GetBytes(confirmMessage);
                            try
                            {
                                thisClient.Send(confirmBuffer);
                            }
                            catch
                            {
                                logs.AppendText("There is a problem when confirmation! Check the connection...\n");
                                terminating = true;
                                connected = false;
                                // BURAYA DISABLED YAP
                                serverSocket.Close();
                            }


                            thisClient.Close();
                            clientSockets.Remove(thisClient);
                        }

                    }


                    Byte[] buffer = new Byte[256];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));
                    logs.AppendText("Client: " + incomingMessage + "\n");
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("A client has disconnected\n");
                    }
                    thisClient.Close();
                    clientSockets.Remove(thisClient);
                    connected = false;
                }
            }
        }


    }
}
