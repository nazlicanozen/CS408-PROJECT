namespace cs408project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            logsIF = new RichTextBox();
            IPBox = new TextBox();
            PortBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ConnectButton = new Button();
            IFSubButton = new Button();
            IFUnsubButton = new Button();
            msgBoxSPS = new TextBox();
            SendIFButton = new Button();
            DisconnectButton = new Button();
            UsernameBox = new TextBox();
            label4 = new Label();
            logsSPS = new RichTextBox();
            label5 = new Label();
            label6 = new Label();
            SPSSubButton = new Button();
            SPSUnsubButton = new Button();
            msgBoxIF = new TextBox();
            SendSPSButton = new Button();
            label3 = new Label();
            label7 = new Label();
            label8 = new Label();
            logs = new RichTextBox();
            SuspendLayout();
            // 
            // logsIF
            // 
            logsIF.Location = new Point(761, 27);
            logsIF.Name = "logsIF";
            logsIF.ReadOnly = true;
            logsIF.Size = new Size(275, 184);
            logsIF.TabIndex = 0;
            logsIF.Text = "";
            // 
            // IPBox
            // 
            IPBox.Location = new Point(100, 22);
            IPBox.Name = "IPBox";
            IPBox.Size = new Size(258, 31);
            IPBox.TabIndex = 1;
            IPBox.TextChanged += IPBox_TextChanged;
            // 
            // PortBox
            // 
            PortBox.Location = new Point(100, 70);
            PortBox.Name = "PortBox";
            PortBox.Size = new Size(258, 31);
            PortBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(9, 25);
            label1.Name = "label1";
            label1.Size = new Size(27, 25);
            label1.TabIndex = 3;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(9, 70);
            label2.Name = "label2";
            label2.Size = new Size(44, 25);
            label2.TabIndex = 4;
            label2.Text = "Port";
            label2.Click += label2_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(381, 25);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(112, 34);
            ConnectButton.TabIndex = 5;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // IFSubButton
            // 
            IFSubButton.Enabled = false;
            IFSubButton.Location = new Point(100, 171);
            IFSubButton.Name = "IFSubButton";
            IFSubButton.Size = new Size(118, 34);
            IFSubButton.TabIndex = 8;
            IFSubButton.Text = "Subscribe";
            IFSubButton.UseVisualStyleBackColor = true;
            IFSubButton.Click += IFSubButton_Click;
            // 
            // IFUnsubButton
            // 
            IFUnsubButton.Enabled = false;
            IFUnsubButton.Location = new Point(300, 171);
            IFUnsubButton.Name = "IFUnsubButton";
            IFUnsubButton.Size = new Size(124, 34);
            IFUnsubButton.TabIndex = 9;
            IFUnsubButton.Text = "Unsubscribe";
            IFUnsubButton.UseVisualStyleBackColor = true;
            IFUnsubButton.Click += IFUnsubButton_Click;
            // 
            // msgBoxSPS
            // 
            msgBoxSPS.Enabled = false;
            msgBoxSPS.Location = new Point(12, 342);
            msgBoxSPS.Name = "msgBoxSPS";
            msgBoxSPS.Size = new Size(484, 31);
            msgBoxSPS.TabIndex = 13;
            // 
            // SendIFButton
            // 
            SendIFButton.Enabled = false;
            SendIFButton.Location = new Point(196, 248);
            SendIFButton.Name = "SendIFButton";
            SendIFButton.Size = new Size(112, 34);
            SendIFButton.TabIndex = 14;
            SendIFButton.Text = "Send";
            SendIFButton.UseVisualStyleBackColor = true;
            SendIFButton.Click += SendIFButton_Click;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Enabled = false;
            DisconnectButton.Location = new Point(381, 70);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(112, 34);
            DisconnectButton.TabIndex = 15;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // UsernameBox
            // 
            UsernameBox.Location = new Point(100, 121);
            UsernameBox.Name = "UsernameBox";
            UsernameBox.Size = new Size(258, 31);
            UsernameBox.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(9, 121);
            label4.Name = "label4";
            label4.Size = new Size(91, 25);
            label4.TabIndex = 17;
            label4.Text = "Username";
            // 
            // logsSPS
            // 
            logsSPS.Location = new Point(761, 256);
            logsSPS.Name = "logsSPS";
            logsSPS.ReadOnly = true;
            logsSPS.Size = new Size(274, 180);
            logsSPS.TabIndex = 18;
            logsSPS.Text = "";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(761, -1);
            label5.Name = "label5";
            label5.Size = new Size(153, 25);
            label5.TabIndex = 19;
            label5.Text = "Messages from IF";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(761, 219);
            label6.Name = "label6";
            label6.Size = new Size(169, 25);
            label6.TabIndex = 20;
            label6.Text = "Messages from SPS";
            // 
            // SPSSubButton
            // 
            SPSSubButton.Enabled = false;
            SPSSubButton.Location = new Point(145, 302);
            SPSSubButton.Name = "SPSSubButton";
            SPSSubButton.Size = new Size(118, 34);
            SPSSubButton.TabIndex = 21;
            SPSSubButton.Text = "Subscribe";
            SPSSubButton.UseVisualStyleBackColor = true;
            SPSSubButton.Click += SPSSubButton_Click;
            // 
            // SPSUnsubButton
            // 
            SPSUnsubButton.Enabled = false;
            SPSUnsubButton.Location = new Point(300, 302);
            SPSUnsubButton.Name = "SPSUnsubButton";
            SPSUnsubButton.Size = new Size(124, 34);
            SPSUnsubButton.TabIndex = 22;
            SPSUnsubButton.Text = "Unsubscribe";
            SPSUnsubButton.UseVisualStyleBackColor = true;
            SPSUnsubButton.Click += SPSUnsubButton_Click;
            // 
            // msgBoxIF
            // 
            msgBoxIF.Enabled = false;
            msgBoxIF.Location = new Point(9, 211);
            msgBoxIF.Name = "msgBoxIF";
            msgBoxIF.Size = new Size(484, 31);
            msgBoxIF.TabIndex = 23;
            // 
            // SendSPSButton
            // 
            SendSPSButton.Enabled = false;
            SendSPSButton.Location = new Point(196, 379);
            SendSPSButton.Name = "SendSPSButton";
            SendSPSButton.Size = new Size(112, 34);
            SendSPSButton.TabIndex = 24;
            SendSPSButton.Text = "Send";
            SendSPSButton.UseVisualStyleBackColor = true;
            SendSPSButton.Click += SendSPSButton_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(13, 175);
            label3.Name = "label3";
            label3.Size = new Size(56, 25);
            label3.TabIndex = 25;
            label3.Text = "IF100";
            label3.Click += label3_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 307);
            label7.Name = "label7";
            label7.Size = new Size(72, 25);
            label7.TabIndex = 26;
            label7.Text = "SPS101";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(524, -1);
            label8.Name = "label8";
            label8.Size = new Size(60, 25);
            label8.TabIndex = 27;
            label8.Text = "Status";
            // 
            // logs
            // 
            logs.Location = new Point(524, 27);
            logs.Name = "logs";
            logs.ReadOnly = true;
            logs.Size = new Size(216, 409);
            logs.TabIndex = 28;
            logs.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1048, 450);
            Controls.Add(logs);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label3);
            Controls.Add(SendSPSButton);
            Controls.Add(msgBoxIF);
            Controls.Add(SPSUnsubButton);
            Controls.Add(SPSSubButton);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(logsSPS);
            Controls.Add(label4);
            Controls.Add(UsernameBox);
            Controls.Add(DisconnectButton);
            Controls.Add(SendIFButton);
            Controls.Add(msgBoxSPS);
            Controls.Add(IFUnsubButton);
            Controls.Add(IFSubButton);
            Controls.Add(ConnectButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PortBox);
            Controls.Add(IPBox);
            Controls.Add(logsIF);
            Name = "Form1";
            Text = "Client GUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox logsIF;
        private TextBox IPBox;
        private TextBox PortBox;
        private Label label1;
        private Label label2;
        private Button ConnectButton;
        private Button IFSubButton;
        private Button IFUnsubButton;
        private TextBox msgBoxSPS;
        private Button SendIFButton;
        private Button DisconnectButton;
        private TextBox UsernameBox;
        private Label label4;
        private RichTextBox logsSPS;
        private Label label5;
        private Label label6;
        private Button SPSSubButton;
        private Button SPSUnsubButton;
        private TextBox msgBoxIF;
        private Button SendSPSButton;
        private Label label3;
        private Label label7;
        private Label label8;
        private RichTextBox logs;
    }
}