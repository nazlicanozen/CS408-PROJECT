namespace cs408projectServer2
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
            label1 = new Label();
            ListenBox = new TextBox();
            label2 = new Label();
            ListenButton = new Button();
            logsConnected = new RichTextBox();
            logsIF = new RichTextBox();
            logsSPS = new RichTextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // logs
            // 
            logs.Location = new Point(47, 66);
            logs.Name = "logs";
            logs.ReadOnly = true;
            logs.Size = new Size(251, 372);
            logs.TabIndex = 0;
            logs.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 28);
            label1.Name = "label1";
            label1.Size = new Size(63, 25);
            label1.TabIndex = 1;
            label1.Text = "Events";
            // 
            // ListenBox
            // 
            ListenBox.Location = new Point(431, 53);
            ListenBox.Name = "ListenBox";
            ListenBox.Size = new Size(188, 31);
            ListenBox.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(341, 53);
            label2.Name = "label2";
            label2.Size = new Size(44, 25);
            label2.TabIndex = 3;
            label2.Text = "Port";
            // 
            // ListenButton
            // 
            ListenButton.Location = new Point(653, 53);
            ListenButton.Name = "ListenButton";
            ListenButton.Size = new Size(112, 34);
            ListenButton.TabIndex = 4;
            ListenButton.Text = "Listen";
            ListenButton.UseVisualStyleBackColor = true;
            ListenButton.Click += ListenButton_Click;
            // 
            // logsConnected
            // 
            logsConnected.Location = new Point(341, 124);
            logsConnected.Name = "logsConnected";
            logsConnected.ReadOnly = true;
            logsConnected.Size = new Size(424, 135);
            logsConnected.TabIndex = 5;
            logsConnected.Text = "";
            // 
            // logsIF
            // 
            logsIF.Location = new Point(341, 294);
            logsIF.Name = "logsIF";
            logsIF.ReadOnly = true;
            logsIF.Size = new Size(187, 144);
            logsIF.TabIndex = 6;
            logsIF.Text = "";
            // 
            // logsSPS
            // 
            logsSPS.Location = new Point(575, 294);
            logsSPS.Name = "logsSPS";
            logsSPS.ReadOnly = true;
            logsSPS.Size = new Size(190, 144);
            logsSPS.TabIndex = 7;
            logsSPS.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(341, 96);
            label3.Name = "label3";
            label3.Size = new Size(97, 25);
            label3.TabIndex = 8;
            label3.Text = "Connected";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(341, 262);
            label4.Name = "label4";
            label4.Size = new Size(176, 25);
            label4.TabIndex = 9;
            label4.Text = "Subscribed to IF 100";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(575, 262);
            label5.Name = "label5";
            label5.Size = new Size(192, 25);
            label5.TabIndex = 10;
            label5.Text = "Subscribed to SPS 101";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(logsSPS);
            Controls.Add(logsIF);
            Controls.Add(logsConnected);
            Controls.Add(ListenButton);
            Controls.Add(label2);
            Controls.Add(ListenBox);
            Controls.Add(label1);
            Controls.Add(logs);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox logs;
        private Label label1;
        private TextBox ListenBox;
        private Label label2;
        private Button ListenButton;
        private RichTextBox logsConnected;
        private RichTextBox logsIF;
        private RichTextBox logsSPS;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}