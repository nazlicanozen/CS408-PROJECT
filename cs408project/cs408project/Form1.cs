using System.Net.Sockets;
using System.Text;

namespace cs408project
{
    public partial class Form1 : Form
    {

        bool terminating = false; // Boolean value used to indicate when the client is being closed
        bool connected = false;   // Boolean value used to indicate when the client is connected to the server.
        Socket clientSocket;      // Socket used to communicate with the server
        bool IFsubbed = false;  
        bool SPSsubbed = false;
        string username = "";     // Username to be used by the client to identify itself to the server.
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
            connected = false; // Set the connected boolean to false
            terminating = true; // Set the terminating boolean to true
            if (clientSocket != null) // This is used to handle the edge case where the client application is closed 
            {                         // while the socket is closed (for example, if we closed the application without attempting any connedtþon)
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
            Environment.Exit(0); // Closes the application.
        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a new socket
            string IP = IPBox.Text; // Gets the IP address from the IP Text box
            username = UsernameBox.Text; // Gets the username from the username Text box

            int portNum; // Integer value used to hold the port of the connection.

            if (IP.Length == 0) { // Edge case when attempting connection without entering anything to IP textbox
                logs.AppendText("Please enter an IP\n");
            }
            else
            {
                if(username.Length == 0) // Edge case when attempting connection without entering anything to username textbox
                {
                    logs.AppendText("Please enter a username\n");

                }
                else
                {

                    if (Int32.TryParse(PortBox.Text, out portNum)) // Try to parse the port number from the port text box.
                    {
                        try
                        {
                            clientSocket.Connect(IP, portNum); // Connect to the server with TCP socket.
                            ConnectButton.Enabled = false; 
                            DisconnectButton.Enabled = true;
                            IPBox.Enabled = false;
                            PortBox.Enabled = false;
                            IFSubButton.Enabled = true;
                            SPSSubButton.Enabled = true;
                            UsernameBox.Enabled = false;
                            connected = true; // Set the connected boolean to true
                            logs.AppendText("Attempting to connect to server...\n"); 
                            Byte[] usernameBuffer = Encoding.Default.GetBytes(username);
                            try
                            {
                                Thread receiveThread = new Thread(Receive); // Create a new thread for receiving messages
                                receiveThread.Start(); // Start the receive thread
                                clientSocket.Send(usernameBuffer); // Sends the username to server for confirmation as valid username
                            }
                            catch //Case if there is problem with the connection to server during confirmation 
                            {
                                logs.AppendText("There is a problem when confirmation! Check the connection...\n");
                                terminating = true;
                                connected = false;
                                // BURAYA DISABLED YAP
                                clientSocket.Close();

                            }
                        }
                        catch // Case if client cannot establish connection with the server.
                        {
                            logs.AppendText("Could not connect to the server!\n");
                        }
                    }
                    else // Case if the value entered to port box cannot be parsed.
                    {
                        logs.AppendText("Invalid port value\n");
                    }

                }
            }


        }

        //This function is called when client wants to disconnect manually (by pressing disconnect button)
        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            connected = false;
            clientSocket.Close();
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
            username = UsernameBox.Text;
            //logs.AppendText("Disconnected from the server\n");
        }

        private void Receive()
        {
            bool serverConfirmed = false; // Boolean used to check if server confirmed username as valid.
            while (connected)
            {
                try
                {
                    if (!serverConfirmed) //Since server has not confirmed username as valid, send username to be confirmed.
                    {
                        Byte[] confirmBuffer = new Byte[256]; 
                        clientSocket.Receive(confirmBuffer); 

                        string confirmMessage = Encoding.Default.GetString(confirmBuffer); 
                        confirmMessage = confirmMessage.Substring(0, confirmMessage.IndexOf('\0'));

                        //Server confirms username as valid
                        if (confirmMessage == "YES")
                        {
                            serverConfirmed = true;
                            logs.AppendText("Successfully connected to server with username " + username + "\n");
                        }
                        //Server confirms username as invalid
                        else if (confirmMessage == "NO")
                        {
                            logs.AppendText("Server connection failed, invalid username\n");
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
                        else //Case to handle when there is connection problem during verification process.
                        {
                            logs.AppendText("Error while confirmation\n");
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

                        /*
                         * ==
                        * The server accepts message in the following format
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


                    }
                }
                catch
                {
                   
                    logs.AppendText("Disconnected from server\n"); 
                    
                    ConnectButton.Enabled = true; 
                    DisconnectButton.Enabled = false;
                    msgBoxIF.Enabled = false; 
                    msgBoxSPS.Enabled = false;
                    SendIFButton.Enabled = false;
                    SendSPSButton.Enabled = false;
                    IFSubButton.Enabled = false;
                    SPSSubButton.Enabled = false;
                    UsernameBox.Enabled = true;
                    
                    clientSocket.Close(); 
                    connected = false;
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