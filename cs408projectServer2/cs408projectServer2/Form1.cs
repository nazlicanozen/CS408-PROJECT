using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Threading.Channels;

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


        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);  // Server socket used to handle TCP socket connections. 
        List<Socket> clientSockets = new List<Socket>(); // List of connected client sockets.
        List<String> usernames = new List<String>();     // List of all connected usernames.
        List<String> IFusernames = new List<String>();   // List of usernames subscribed to IF 100 channel.
        List<String> SPSusernames = new List<String>();  // List of usernames subscribed to SPS 101 channel.

        List<(Socket,string)> socketUserPairList = new List<(Socket,String)>(); // List of socket username pairs used to identify which username belongs to what socket. 



        bool terminating = false;       // Boolean value used to indicate when the server is being closed
        bool listening = false;         // Boolean value used to indicate when the server is currently listening to the provided port value.


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

                    Thread receiveThread = new Thread(() => Receive(newClient)); // After socket is opened, Recieve thread is started for that particular socket.
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

        // Function used to update the logsConnected box, called when usernames list is updated
        private void updateUsernamesConnectedList()
        {
            logsConnected.Clear();
            foreach (String username in usernames)
            {
                logsConnected.AppendText(username);
                logsConnected.AppendText("\n");
            }            
        }

        // Function used to update the logsSPS box, called when the SPSUsernames list (users subscribed to SPS 101 channel) is updated
        private void updateSPSSubbedList()
        {
            logsSPS.Clear();
            foreach (String username in SPSusernames)
            {
                logsSPS.AppendText(username);
                logsSPS.AppendText("\n");
            }
        }

        // Function used to update the logsSPS box, called when the IFUsernames list (users subscribed to IF 100 channel) is updated
        private void updateIFSubbedList()
        {
            logsIF.Clear();
            foreach (String username in IFusernames)
            {
                logsIF.AppendText(username);
                logsIF.AppendText("\n");
            }
        }

        
        private void Receive(Socket thisClient)
        {
            bool connected = true;      // Boolean used to determine if the current socket is connected to the server.
            bool sentUsername = false;  // Boolean used to detemrine if the current client has sent username to be verified by the server.


            while (connected && !terminating)
            {
                try
                {
                    
                    if (!sentUsername)
                    {
                        Byte[] UsernameBuffer = new Byte[256];
                        thisClient.Receive(UsernameBuffer);
                        string incomingUsername = Encoding.Default.GetString(UsernameBuffer);
                        incomingUsername = incomingUsername.Substring(0, incomingUsername.IndexOf('\0'));
                        logs.AppendText("Incoming user : " + incomingUsername + "\n");

                        if (usernames.Contains(incomingUsername)) //Duplicate usernames
                        {
                            connected = false;

                            string confirmMessage = "NO";
                            Byte[] confirmBuffer = Encoding.Default.GetBytes(confirmMessage);
                            try
                            {
                                thisClient.Send(confirmBuffer);
                            }
                            catch //Case if there is problem with the connection to client during confirmation
                            {
                                logs.AppendText("There is a problem when confirmation! Check the connection...\n");
                                terminating = true;
                                connected = false;
                            }

                            logs.AppendText("Username " + incomingUsername + " attempted to connect, but already exists\n");


                            thisClient.Close();
                            clientSockets.Remove(thisClient);
                        }
                        else //Unique username
                        {
                            usernames.Add(incomingUsername);
                            socketUserPairList.Add((thisClient, incomingUsername));
                            string confirmMessage = "YES";
                            Byte[] confirmBuffer = Encoding.Default.GetBytes(confirmMessage);
                            try
                            {
                                thisClient.Send(confirmBuffer);
                            }
                            catch //Case if there is problem with the connection to client during sending confirmation message
                            {
                                logs.AppendText("There is a problem when confirmation! Check the connection...\n");
                                terminating = true;
                                connected = false;
                            }
                            sentUsername = true;
                            logs.AppendText("Username " + incomingUsername + " has connected!\n");
                            updateUsernamesConnectedList();

                        }

                    }
                    else //Username confirmed, awaiting commands from the client.
                    {
                        Byte[] commandBuffer = new Byte[256];
                        thisClient.Receive(commandBuffer);
                        string incomingCommand = Encoding.Default.GetString(commandBuffer);
                        incomingCommand = incomingCommand.Substring(0, incomingCommand.IndexOf('\0'));

                        /*
                         * ==
                        * The client sends and the server accepts commands in the following format
                        * COMMAND|USERNAME|CHANNEL|MESSAGE
                        * where the '|' character is used delimiter. The command is then parsed utilizing this format. 
                        * ==
                        * -
                        * COMMAND value can take 3 valid values, SUB,UNSUB,MSG
                        *
                        * If COMMAND is SUB, then the user who sent the message is attempting to subscribe to a particular channel
                        * this channel is determined by the value in the CHANNEL field.
                        * If COMMAND is UNSUB, then the user who sent the message is attempting to unsubscribe from a particular channel
                        * this channel is determined by the value in the CHANNEL field.
                        * IF COMMAND is MSG, then the user who sent the messagge is attempting to send a message to a particular channel
                        * The message contents are determined by the MESSAGE field, the channel this message is being send to is determined by the CHANNEL field.
                        * -
                        * USERNAME value can be any string value.
                        * -
                        * CHANNEL value can be one of the 2 given channel name:
                        * IF (used to identify the IF 100 channel)
                        * SPS (used to identify the SPS 101 channel)
                        * -
                        * MESSAGE can be any string value.
                        * ==
                        */

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
                                logs.AppendText("User" + username + " has subscribed to IF 100\n");
                                updateIFSubbedList();
                            }
                            else if (channel == "SPS")
                            {
                                SPSusernames.Add(username);
                                logs.AppendText("User" + username + " has subscribed to SPS 101\n");
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
                                logs.AppendText("User" + username + " has unsubscribed from IF 100\n");
                                updateIFSubbedList();
                            }
                            else if (channel == "SPS")
                            {
                                SPSusernames.Remove(username);
                                logs.AppendText("User" + username + " has unsubscribed from SPS 101\n");
                                updateSPSSubbedList();
                            }
                            else
                            {
                                logs.AppendText("INCORRECT COMMAND: " + command + "|" + username + "|" + channel + "|" + msg + "\n");
                            }
                        }
                        else if (command == "MSG")
                        {
                            if (channel == "IF")
                            {
                                foreach(string it_username in IFusernames)
                                {
                                    foreach((Socket,string) pair in socketUserPairList)
                                    {
                                        if (pair.Item2 == it_username)
                                        {
                                            String server_message = username + "|" + channel + "|" + msg;
                                            byte[] server_message_buffer = Encoding.Default.GetBytes(server_message);
                                            Socket mySocket = pair.Item1;
                                            mySocket.Send(server_message_buffer);
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (channel == "SPS")
                            {
                                foreach (string it_username in SPSusernames)
                                {
                                    foreach ((Socket, string) pair in socketUserPairList)
                                    {
                                        if (pair.Item2 == it_username)
                                        {
                                            String server_message = username + "|" + channel + "|" + msg;
                                            byte[] server_message_buffer = Encoding.Default.GetBytes(server_message);
                                            Socket mySocket = pair.Item1;
                                            mySocket.Send(server_message_buffer);
                                            break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                logs.AppendText("INCORRECT COMMAND: " + command + "|" + username + "|" + channel + "|" + msg + "\n");
                            }
                        }
                        else
                        {
                            logs.AppendText("INCORRECT COMMAND: " + command + "|" + username + "|" + channel + "|" + msg + "\n");
                        }



                    }
                }
                catch
                {
                    if (terminating)
                    {
                        foreach ((Socket, string) pair in socketUserPairList)
                        {
                            if (pair.Item1 == thisClient)
                            {

                                string incomingUsername = pair.Item2;
                                logs.AppendText("Disconnecting user: " + incomingUsername + "\n");
                                usernames.Remove(incomingUsername);
                                socketUserPairList.Remove(pair);
                                IFusernames.Remove(incomingUsername);
                                SPSusernames.Remove(incomingUsername);
                                updateUsernamesConnectedList();
                                updateIFSubbedList();
                                updateSPSSubbedList();
                                break;
                            }
                        }

                    }
                    else
                    {
                        foreach ((Socket, string) pair in socketUserPairList)
                        {
                            if (pair.Item1 == thisClient)
                            {

                                string incomingUsername = pair.Item2;
                                logs.AppendText("User " + incomingUsername + " has disconnected\n");
                                usernames.Remove(incomingUsername);
                                socketUserPairList.Remove(pair);
                                IFusernames.Remove(incomingUsername);
                                SPSusernames.Remove(incomingUsername);
                                updateUsernamesConnectedList();
                                updateIFSubbedList();
                                updateSPSSubbedList();
                                break;
                            }
                        }
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
                serverSocket.Listen(25);

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