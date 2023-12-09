using System.Net.Sockets;
using System.Text;

namespace cs408project
{
    public partial class Form1 : Form
    {

        bool terminating = false; // A flag to indicate if the client is terminating
        bool connected = false;   // A flag to indicate if the client is connected to the server
        Socket clientSocket;      // Socket for communication with the server
        bool IFselected = false;
        bool SPSselected = false;
        bool IFsubbed = false;
        bool SPSsubbed = false;
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
            IFselected = false;
            SPSselected = false;
            IFsubbed = false;
            SPSsubbed = false;
            IFButton.Enabled = false;
            SPSButton.Enabled = false;
            SubButton.Enabled = false;
            UnsubButton.Enabled = false;
            MsgBox.Enabled = false;
            SendButton.Enabled = false;
            UsernameBox.Enabled = false;
            Environment.Exit(0); // Terminate the application
        }


        private void ConnectButton_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // Create a new socket
            string IP = IPBox.Text; // Get the IP address from the text box
            string username = UsernameBox.Text;
            
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
                    IFButton.Enabled = true;
                    SPSButton.Enabled = true;
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

        private void SPSButton_CheckedChanged(object sender, EventArgs e)
        {
            SPSselected = true;
            IFselected = false;
            if (SPSsubbed)
            {
                DisconnectButton.Enabled = true;
                SubButton.Enabled = false;
                MsgBox.Enabled = true;
                SendButton.Enabled = true;
            }
            else
            {
                DisconnectButton.Enabled = false;
                SubButton.Enabled = true;
                MsgBox.Enabled = false;
                SendButton.Enabled = false;
            }
        }

        private void IFButton_CheckedChanged(object sender, EventArgs e)
        {
            IFselected = true;
            SPSselected = false;
            if (IFsubbed)
            {
                DisconnectButton.Enabled = true;
                SubButton.Enabled = false;
                MsgBox.Enabled = true;
                SendButton.Enabled = true;
            }
            else
            {
                DisconnectButton.Enabled = false;
                SubButton.Enabled = true;
                MsgBox.Enabled = false;
                SendButton.Enabled = false;
            }
        }

        private void SubButton_Click(object sender, EventArgs e)
        {
            //Sunucuya iletmen lazým 
            if (IFselected)
            {
                IFsubbed = true;
            }
            else
            {
                SPSsubbed = true;
            }
            UnsubButton.Enabled = true;

        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            connected = false;
            clientSocket.Close();
            ConnectButton.Enabled = true;
            //Sunucuya unsub uyarýsý ver
            IFselected = false;
            SPSselected = false;
            IFsubbed = false;
            SPSsubbed = false;
            IFButton.Enabled = false;
            SPSButton.Enabled = false;
            SubButton.Enabled = false;
            UnsubButton.Enabled = false;
            MsgBox.Enabled = false;
            SendButton.Enabled = false;
            UsernameBox.Enabled = true;
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
                        }
                        else
                        {
                            logs.AppendText("Error while confirmation");
                            clientSocket.Close();
                            connected = false;
                        }

                    }
                    else
                    {
                        Byte[] buffer = new Byte[256]; // Create a buffer for receiving data
                        clientSocket.Receive(buffer); // Receive data from the server

                        string incomingMessage = Encoding.Default.GetString(buffer); // Convert received bytes to a string
                        incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0")); // Remove null characters

                        logs.AppendText("Server: " + incomingMessage + "\n"); // Display the received message
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

    }

}