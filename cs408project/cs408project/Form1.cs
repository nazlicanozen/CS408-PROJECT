using System.Net.Sockets;
using System.Text;

namespace cs408project
{
    public partial class Form1 : Form
    {

        bool terminating = false; // A flag to indicate if the client is terminating
        bool connected = false;   // A flag to indicate if the client is connected to the server
        Socket clientSocket;      // Socket for communication with the server
        bool IFsubbed = false;
        bool SPSsubbed = false;
        string username = "";
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false; // Allow cross-thread UI updates
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing); // Attach a handler to the form closing event
            InitializeComponent(); // Initialize the form's components
        }

        private void IPBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false; // Set the connected flag to false
            terminating = true; // Set the terminating flag to true
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            ConnectButton.Enabled = false;
            //Sunucuya unsub uyarýsý ver
            IFsubbed = false;
            SPSsubbed = false;
            IFSubButton.Enabled = false;
            SPSSubButton.Enabled = false;
            IFSubButton.Enabled = false;
            IFUnsubButton.Enabled = false;
            msgBoxSPS.Enabled = false;
            SendIFButton.Enabled = false;
            UsernameBox.Enabled = false;
            Environment.Exit(0); // Terminate the application
        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a new socket
            string IP = IPBox.Text; // Get the IP address from the text box
            username = UsernameBox.Text;

            int portNum;
            if (Int32.TryParse(PortBox.Text, out portNum)) // Try to parse the port number from the text box
            {
                try
                {
                    clientSocket.Connect(IP, portNum); // Connect to the server
                    ConnectButton.Enabled = false; // Disable the connect button
                    DisconnectButton.Enabled = true;
                    //MsgBox.Enabled = true; // Enable the message text box
                    //SendButton.Enabled = true; 
                    IFSubButton.Enabled = true;
                    SPSSubButton.Enabled = true;
                    UsernameBox.Enabled = false;
                    connected = true; // Set the connected flag to true
                    logs.AppendText("Connected to the server!\n"); // Display a connection message

                    Byte[] usernameBuffer = Encoding.Default.GetBytes(username);
                    try
                    {
                        Thread receiveThread = new Thread(Receive); // Create a new thread for receiving messages
                        receiveThread.Start(); // Start the receive thread
                        clientSocket.Send(usernameBuffer);
                    }
                    catch
                    {
                        logs.AppendText("There is a problem when confirmation! Check the connection...\n");
                        terminating = true;
                        connected = false;
                        // BURAYA DISABLED YAP
                        clientSocket.Close();

                    }
                }
                catch
                {
                    logs.AppendText("Could not connect to the server!\n"); // Display an error message
                }
            }
        }


        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            connected = false;
            clientSocket.Close();
            ConnectButton.Enabled = true;
            DisconnectButton.Enabled = false;
            //Sunucuya unsub uyarýsý ver
            IFsubbed = false;
            SPSsubbed = false;
            IFSubButton.Enabled = false;
            SPSSubButton.Enabled = false;
            IFSubButton.Enabled = false;
            IFUnsubButton.Enabled = false;
            msgBoxSPS.Enabled = false;
            SendIFButton.Enabled = false;
            UsernameBox.Enabled = true;
            username = UsernameBox.Text;
            logs.AppendText(username + " has disconnected\n");
        }

        private void Receive()
        {
            bool serverConfirmed = false;
            while (connected)
            {
                try
                {
                    if (!serverConfirmed)
                    {
                        Byte[] confirmBuffer = new Byte[256]; // Create a buffer for receiving data
                        clientSocket.Receive(confirmBuffer); // Receive data from the server

                        string confirmMessage = Encoding.Default.GetString(confirmBuffer); // Convert received bytes to a string
                        confirmMessage = confirmMessage.Substring(0, confirmMessage.IndexOf('\0')); // Remove null characters

                        logs.AppendText(confirmMessage);

                        if (confirmMessage == "YES")
                        {
                            serverConfirmed = true;
                        }
                        else if (confirmMessage == "NO")
                        {
                            logs.AppendText("Server connection failed, invalid username");
                            clientSocket.Close();
                            connected = false;
                            ConnectButton.Enabled = true;
                            DisconnectButton.Enabled = false;
                            IFsubbed = false;
                            SPSsubbed = false;
                            IFSubButton.Enabled = false;
                            SPSSubButton.Enabled = false;
                            IFSubButton.Enabled = false;
                            IFUnsubButton.Enabled = false;
                            msgBoxSPS.Enabled = false;
                            SendIFButton.Enabled = false;
                            UsernameBox.Enabled = true;

                        }
                        else
                        {
                            logs.AppendText("Error while confirmation");
                            clientSocket.Close();
                            connected = false;
                            ConnectButton.Enabled = true;
                            DisconnectButton.Enabled = false;
                            IFsubbed = false;
                            SPSsubbed = false;
                            IFSubButton.Enabled = false;
                            SPSSubButton.Enabled = false;
                            IFSubButton.Enabled = false;
                            IFUnsubButton.Enabled = false;
                            msgBoxSPS.Enabled = false;
                            SendIFButton.Enabled = false;
                            UsernameBox.Enabled = true;
                        }

                    }
                    else
                    {

                        Byte[] commandBuffer = new Byte[256];
                        clientSocket.Receive(commandBuffer);
                        string incomingCommand = Encoding.Default.GetString(commandBuffer);
                        incomingCommand = incomingCommand.Substring(0, incomingCommand.IndexOf('\0'));

                        string username = "";
                        string channel = "";
                        string msg = "";

                        username = incomingCommand.Substring(0, incomingCommand.IndexOf("|"));
                        incomingCommand = incomingCommand.Substring(incomingCommand.IndexOf("|") + 1);

                        channel = incomingCommand.Substring(0, incomingCommand.IndexOf("|"));
                        incomingCommand = incomingCommand.Substring(incomingCommand.IndexOf("|") + 1);

                        msg = incomingCommand;

                        if (channel == "IF")
                        {
                            string message = username + " says: " + msg + "\n";
                            logsIF.AppendText(message);
                        }
                        else if (channel == "SPS")
                        {
                            string message = username + " says: " + msg + "\n";
                            logsSPS.AppendText(message);
                        }
                        else
                        {
                            logs.AppendText("Incorrect message format: " + username + "|" + channel + "|" + msg + "\n");
                        }


                        /*
                        Byte[] buffer = new Byte[256]; // Create a buffer for receiving data
                        clientSocket.Receive(buffer); // Receive data from the server

                        string incomingMessage = Encoding.Default.GetString(buffer); // Convert received bytes to a string
                        incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0")); // Remove null characters

                        logs.AppendText("Server: " + incomingMessage + "\n"); // Display the received message
                        */
                    }
                }
                catch
                {
                    if (terminating)
                    {
                        logs.AppendText("The server has disconnected\n"); // Display a disconnection message
                        /*
                        button_connect.Enabled = true; // Enable the connect button
                        textBox_message.Enabled = false; // Disable the message text box
                        button_send.Enabled = false; // Disable the send button
                        */
                    }

                    clientSocket.Close(); // Close the socket
                    connected = false; // Set the connected flag to false
                }
            }
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        

        /*
        private void SendButton_Click(object sender, EventArgs e)
        {
            String message = msgBoxSPS.Text;
            if (IFselected)
            {
                String IFCommand = "MSG|" + username + "|IF|" + message;
                byte[] IFCommand_buffer = Encoding.Default.GetBytes(IFCommand);
                clientSocket.Send(IFCommand_buffer);
            }
            else if (SPSselected)
            {
                String SPSCommand = "MSG|" + username + "|IF|" + message;
                byte[] SPSCommand_buffer = Encoding.Default.GetBytes(SPSCommand);
                clientSocket.Send(SPSCommand_buffer);
            }
            else
            {
                logsIF.AppendText("Incalid msg");
            }
        }
        */



        private void label3_Click(object sender, EventArgs e)
        {

        }


        private void IFSubButton_Click(object sender, EventArgs e)
        {
            IFsubbed = true;
            String IFCommand = "SUB|" + username + "|IF|";
            byte[] IFCommand_buffer = Encoding.Default.GetBytes(IFCommand);
            clientSocket.Send(IFCommand_buffer);

            IFUnsubButton.Enabled = true;
            IFSubButton.Enabled = false;

            msgBoxIF.Enabled = true;
            SendIFButton.Enabled = true;
        }
        private void IFUnsubButton_Click(object sender, EventArgs e)
        {
            IFsubbed = false;
            String IFCommand = "UNSUB|" + username + "|IF|";
            byte[] IFCommand_buffer = Encoding.Default.GetBytes(IFCommand);
            clientSocket.Send(IFCommand_buffer);

            IFUnsubButton.Enabled = false;
            IFSubButton.Enabled = true;

            msgBoxIF.Enabled = false;
            SendIFButton.Enabled = false;
        }

        private void SendIFButton_Click(object sender, EventArgs e)
        {
            String message = msgBoxIF.Text;
            String IFCommand = "MSG|" + username + "|IF|" + message;
            byte[] IFCommand_buffer = Encoding.Default.GetBytes(IFCommand);
            clientSocket.Send(IFCommand_buffer);
        }

        private void SPSSubButton_Click(object sender, EventArgs e)
        {

            SPSsubbed = true;
            String SPSCommand = "SUB|" + username + "|SPS|";
            byte[] SPSCommand_buffer = Encoding.Default.GetBytes(SPSCommand);
            clientSocket.Send(SPSCommand_buffer);

            SPSUnsubButton.Enabled = true;
            SPSSubButton.Enabled = false;

            msgBoxSPS.Enabled = true;
            SendSPSButton.Enabled = true;

        }

        private void SPSUnsubButton_Click(object sender, EventArgs e)
        {

            SPSsubbed = false;
            String SPSCommand = "UNSUB|" + username + "|SPS|";
            byte[] SPSCommand_buffer = Encoding.Default.GetBytes(SPSCommand);
            clientSocket.Send(SPSCommand_buffer);
            SPSUnsubButton.Enabled = false;
            SPSSubButton.Enabled = true;
            msgBoxSPS.Enabled = false;
            SendSPSButton.Enabled = false;
        }

        private void SendSPSButton_Click(object sender, EventArgs e)
        {
            String message = msgBoxSPS.Text;
            String SPSCommand = "MSG|" + username + "|SPS|" + message;
            byte[] SPSCommand_buffer = Encoding.Default.GetBytes(SPSCommand);
            clientSocket.Send(SPSCommand_buffer);
        }
    }

}