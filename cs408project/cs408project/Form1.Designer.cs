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
            logs = new RichTextBox();
            IPBox = new TextBox();
            PortBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ConnectButton = new Button();
            SubButton = new Button();
            UnsubButton = new Button();
            IFButton = new RadioButton();
            SPSButton = new RadioButton();
            label3 = new Label();
            MsgBox = new TextBox();
            SendButton = new Button();
            DisconnectButton = new Button();
            UsernameBox = new TextBox();
            label4 = new Label();
            SuspendLayout();
            // 
            // logs
            // 
            logs.Location = new Point(502, 37);
            logs.Name = "logs";
            logs.ReadOnly = true;
            logs.Size = new Size(275, 301);
            logs.TabIndex = 0;
            logs.Text = "";
            // 
            // IPBox
            // 
            IPBox.Location = new Point(103, 45);
            IPBox.Name = "IPBox";
            IPBox.Size = new Size(258, 31);
            IPBox.TabIndex = 1;
            IPBox.TextChanged += IPBox_TextChanged;
            // 
            // PortBox
            // 
            PortBox.Location = new Point(103, 93);
            PortBox.Name = "PortBox";
            PortBox.Size = new Size(258, 31);
            PortBox.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 48);
            label1.Name = "label1";
            label1.Size = new Size(27, 25);
            label1.TabIndex = 3;
            label1.Text = "IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 93);
            label2.Name = "label2";
            label2.Size = new Size(44, 25);
            label2.TabIndex = 4;
            label2.Text = "Port";
            label2.Click += label2_Click;
            // 
            // ConnectButton
            // 
            ConnectButton.Location = new Point(384, 48);
            ConnectButton.Name = "ConnectButton";
            ConnectButton.Size = new Size(112, 34);
            ConnectButton.TabIndex = 5;
            ConnectButton.Text = "Connect";
            ConnectButton.UseVisualStyleBackColor = true;
            ConnectButton.Click += ConnectButton_Click;
            // 
            // SubButton
            // 
            SubButton.Enabled = false;
            SubButton.Location = new Point(214, 275);
            SubButton.Name = "SubButton";
            SubButton.Size = new Size(118, 34);
            SubButton.TabIndex = 8;
            SubButton.Text = "Subscribe";
            SubButton.UseVisualStyleBackColor = true;
            SubButton.Click += SubButton_Click;
            // 
            // UnsubButton
            // 
            UnsubButton.Enabled = false;
            UnsubButton.Location = new Point(372, 275);
            UnsubButton.Name = "UnsubButton";
            UnsubButton.Size = new Size(124, 34);
            UnsubButton.TabIndex = 9;
            UnsubButton.Text = "Unsubscribe";
            UnsubButton.UseVisualStyleBackColor = true;
            // 
            // IFButton
            // 
            IFButton.AutoSize = true;
            IFButton.Enabled = false;
            IFButton.Location = new Point(22, 251);
            IFButton.Name = "IFButton";
            IFButton.Size = new Size(86, 29);
            IFButton.TabIndex = 10;
            IFButton.TabStop = true;
            IFButton.Text = "IF 100";
            IFButton.UseVisualStyleBackColor = true;
            IFButton.CheckedChanged += IFButton_CheckedChanged;
            // 
            // SPSButton
            // 
            SPSButton.AutoSize = true;
            SPSButton.Enabled = false;
            SPSButton.Location = new Point(22, 309);
            SPSButton.Name = "SPSButton";
            SPSButton.Size = new Size(102, 29);
            SPSButton.TabIndex = 11;
            SPSButton.TabStop = true;
            SPSButton.Text = "SPS 101";
            SPSButton.UseVisualStyleBackColor = true;
            SPSButton.CheckedChanged += SPSButton_CheckedChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 196);
            label3.Name = "label3";
            label3.Size = new Size(154, 25);
            label3.TabIndex = 12;
            label3.Text = "Choose a Channel";
            // 
            // MsgBox
            // 
            MsgBox.Enabled = false;
            MsgBox.Location = new Point(34, 376);
            MsgBox.Name = "MsgBox";
            MsgBox.Size = new Size(462, 31);
            MsgBox.TabIndex = 13;
            // 
            // SendButton
            // 
            SendButton.Enabled = false;
            SendButton.Location = new Point(585, 376);
            SendButton.Name = "SendButton";
            SendButton.Size = new Size(112, 34);
            SendButton.TabIndex = 14;
            SendButton.Text = "Send";
            SendButton.UseVisualStyleBackColor = true;
            // 
            // DisconnectButton
            // 
            DisconnectButton.Enabled = false;
            DisconnectButton.Location = new Point(384, 93);
            DisconnectButton.Name = "DisconnectButton";
            DisconnectButton.Size = new Size(112, 34);
            DisconnectButton.TabIndex = 15;
            DisconnectButton.Text = "Disconnect";
            DisconnectButton.UseVisualStyleBackColor = true;
            DisconnectButton.Click += DisconnectButton_Click;
            // 
            // UsernameBox
            // 
            UsernameBox.Location = new Point(103, 144);
            UsernameBox.Name = "UsernameBox";
            UsernameBox.Size = new Size(258, 31);
            UsernameBox.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 144);
            label4.Name = "label4";
            label4.Size = new Size(91, 25);
            label4.TabIndex = 17;
            label4.Text = "Username";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(UsernameBox);
            Controls.Add(DisconnectButton);
            Controls.Add(SendButton);
            Controls.Add(MsgBox);
            Controls.Add(label3);
            Controls.Add(SPSButton);
            Controls.Add(IFButton);
            Controls.Add(UnsubButton);
            Controls.Add(SubButton);
            Controls.Add(ConnectButton);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(PortBox);
            Controls.Add(IPBox);
            Controls.Add(logs);
            Name = "Form1";
            Text = "Client GUI";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox logs;
        private TextBox IPBox;
        private TextBox PortBox;
        private Label label1;
        private Label label2;
        private Button ConnectButton;
        private Button SubButton;
        private Button UnsubButton;
        private RadioButton IFButton;
        private RadioButton SPSButton;
        private Label label3;
        private TextBox MsgBox;
        private Button SendButton;
        private Button DisconnectButton;
        private TextBox UsernameBox;
        private Label label4;
    }
}