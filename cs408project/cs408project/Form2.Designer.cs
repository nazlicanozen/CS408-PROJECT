namespace cs408project
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            logs = new RichTextBox();
            ListenButton = new Button();
            label1 = new Label();
            ListenBox = new TextBox();
            logsIF = new RichTextBox();
            logsSPS = new RichTextBox();
            logsConnected = new RichTextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            SuspendLayout();
            // 
            // logs
            // 
            logs.Location = new Point(27, 44);
            logs.Name = "logs";
            logs.Size = new Size(238, 394);
            logs.TabIndex = 0;
            logs.Text = "";
            // 
            // ListenButton
            // 
            ListenButton.Location = new Point(587, 33);
            ListenButton.Name = "ListenButton";
            ListenButton.Size = new Size(112, 34);
            ListenButton.TabIndex = 1;
            ListenButton.Text = "Listen";
            ListenButton.UseVisualStyleBackColor = true;
            ListenButton.Click += ListenButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(307, 36);
            label1.Name = "label1";
            label1.Size = new Size(44, 25);
            label1.TabIndex = 2;
            label1.Text = "Port";
            label1.Click += label1_Click;
            // 
            // ListenBox
            // 
            ListenBox.Location = new Point(381, 36);
            ListenBox.Name = "ListenBox";
            ListenBox.Size = new Size(184, 31);
            ListenBox.TabIndex = 3;
            ListenBox.TextChanged += textBox1_TextChanged;
            // 
            // logsIF
            // 
            logsIF.Location = new Point(307, 283);
            logsIF.Name = "logsIF";
            logsIF.Size = new Size(167, 144);
            logsIF.TabIndex = 4;
            logsIF.Text = "";
            logsIF.TextChanged += richTextBox2_TextChanged;
            // 
            // logsSPS
            // 
            logsSPS.Location = new Point(529, 283);
            logsSPS.Name = "logsSPS";
            logsSPS.Size = new Size(170, 144);
            logsSPS.TabIndex = 5;
            logsSPS.Text = "";
            // 
            // logsConnected
            // 
            logsConnected.Location = new Point(307, 101);
            logsConnected.Name = "logsConnected";
            logsConnected.Size = new Size(392, 144);
            logsConnected.TabIndex = 6;
            logsConnected.Text = "";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(307, 73);
            label2.Name = "label2";
            label2.Size = new Size(97, 25);
            label2.TabIndex = 7;
            label2.Text = "Connected";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(307, 255);
            label3.Name = "label3";
            label3.Size = new Size(176, 25);
            label3.TabIndex = 8;
            label3.Text = "Subscribed to IF 100";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(529, 255);
            label4.Name = "label4";
            label4.Size = new Size(192, 25);
            label4.TabIndex = 9;
            label4.Text = "Subscribed to SPS 101";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(27, 9);
            label5.Name = "label5";
            label5.Size = new Size(63, 25);
            label5.TabIndex = 10;
            label5.Text = "Events";
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(logsConnected);
            Controls.Add(logsSPS);
            Controls.Add(logsIF);
            Controls.Add(ListenBox);
            Controls.Add(label1);
            Controls.Add(ListenButton);
            Controls.Add(logs);
            Name = "Form2";
            Text = "Form2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox logs;
        private Button ListenButton;
        private Label label1;
        private TextBox ListenBox;
        private RichTextBox logsIF;
        private RichTextBox logsSPS;
        private RichTextBox logsConnected;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}