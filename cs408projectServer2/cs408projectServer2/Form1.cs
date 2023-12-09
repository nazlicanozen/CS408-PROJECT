using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace cs408projectServer2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //connected = false; // Set the connected flag to false
            terminating = true; // Set the terminating flag to true
            
            Environment.Exit(0); // Terminate the application
        }



        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void logsConnected_TextChanged(object sender, EventArgs e)
        {

        }


        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();
        List<String> usernames = new List<String>();
        List<String> IFusernames = new List<String>();
        List<String> SPSusernames = new List<String>();

        List<(Socket,string)> socketUserPairList = new List<(Socket,String)>();




        bool terminating = false;
        bool listening = false;


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

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

        private void updateUsernamesConnectedList()
        {
            logsConnected.Clear();
            foreach (String username in usernames)
            {
                logsConnected.AppendText(username);
                logsConnected.AppendText("\n");
            }            
        }


        private void updateSPSSubbedList()
        {
            logsSPS.Clear();
            foreach (String username in SPSusernames)
            {
                logsSPS.AppendText(username);
                logsSPS.AppendText("\n");
            }
        }


        private void updateIFSubbedList()
        {
            logsIF.Clear();
            foreach (String username in IFusernames)
            {
                logsIF.AppendText(username);
                logsIF.AppendText("\n");
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
                    //logs.AppendText("TRYA GIRDIM");
                    if (!sentUsername)
                    {
                        Byte[] UsernameBuffer = new Byte[256];
                        thisClient.Receive(UsernameBuffer);
                        string incomingUsername = Encoding.Default.GetString(UsernameBuffer);
                        incomingUsername = incomingUsername.Substring(0, incomingUsername.IndexOf('\0'));
                        logs.AppendText("Incoming user : " + incomingUsername + "\n");

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

                            logs.AppendText("Username " + incomingUsername + " attempted to connect, but already exists\n");


                            thisClient.Close();
                            clientSockets.Remove(thisClient);
                        }
                        else
                        {
                            usernames.Add(incomingUsername);
                            socketUserPairList.Add((thisClient, incomingUsername));
                            string confirmMessage = "YES";
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
                            sentUsername = true;
                            /*
                            logs.AppendText("Username " + incomingUsername + " has connected!");
                            logs.AppendText("USERNAMES\n");
                            foreach (string username in usernames)
                            {
                                logs.AppendText(username);
                            }
                            logs.AppendText("====\n");
                            */
                            logs.AppendText("Username " + incomingUsername + " has connected!\n");
                            logs.AppendText("PAIR LIST:\n");
                            foreach ( (Socket,string) pair in socketUserPairList)
                            {
                                logs.AppendText("Socket: " + pair.Item1.ToString() + " Username: " + pair.Item2.ToString() + "\n");
                            }

                            updateUsernamesConnectedList();

                        }

                    }
                    else
                    {
                        Byte[] commandBuffer = new Byte[256];
                        thisClient.Receive(commandBuffer);
                        string incomingCommand = Encoding.Default.GetString(commandBuffer);
                        incomingCommand = incomingCommand.Substring(0, incomingCommand.IndexOf('\0'));

                        string command = "";
                        string username = "";
                        string channel = "";
                        string msg = "";

                        command = incomingCommand.Substring(0,incomingCommand.IndexOf("|"));
                        incomingCommand = incomingCommand.Substring(incomingCommand.IndexOf("|")+1);

                        username = incomingCommand.Substring(0, incomingCommand.IndexOf("|"));
                        incomingCommand = incomingCommand.Substring(incomingCommand.IndexOf("|")+1);

                        channel = incomingCommand.Substring(0, incomingCommand.IndexOf("|"));
                        incomingCommand = incomingCommand.Substring(incomingCommand.IndexOf("|") + 1);

                        msg = incomingCommand;

                        if(command == "SUB")
                        {
                            if (channel == "IF")
                            {
                                IFusernames.Add(username);
                                updateIFSubbedList();
                            }
                            else if (channel == "SPS")
                            {
                                SPSusernames.Add(username);
                                updateSPSSubbedList();
                            }
                            else
                            {
                                logs.AppendText("INCORRECT COMMAND: " + command + "|" + username + "|" + channel + "|" + msg + "\n");
                            }
                        }
                        else if(command == "UNSUB")
                        {
                            if (channel == "IF")
                            {
                                IFusernames.Remove(username);
                                updateIFSubbedList();
                            }
                            else if (channel == "SPS")
                            {
                                SPSusernames.Remove(username);
                                updateSPSSubbedList();
                            }
                            else
                            {
                                logs.AppendText("INCORRECT COMMAND: " + command + "|" + username + "|" + channel + "|" + msg + "\n");
                            }
                        }
                        else
                        {
                            logs.AppendText("OK");
                        }

                        /*
                        logs.AppendText("Client: " + incomingMessage + "\n");
                        */


                    }
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
    }

}