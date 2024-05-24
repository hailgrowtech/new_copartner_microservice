namespace EncryptionService
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
            label1 = new Label();
            txtPlainText = new TextBox();
            btnEncrypt = new Button();
            label2 = new Label();
            txtEncryptedText = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(29, 50);
            label1.Name = "label1";
            label1.Size = new Size(84, 25);
            label1.TabIndex = 0;
            label1.Text = "Plain Text";
            // 
            // txtPlainText
            // 
            txtPlainText.Location = new Point(177, 44);
            txtPlainText.Name = "txtPlainText";
            txtPlainText.Size = new Size(1067, 31);
            txtPlainText.TabIndex = 1;
            // 
            // btnEncrypt
            // 
            btnEncrypt.Location = new Point(1281, 41);
            btnEncrypt.Name = "btnEncrypt";
            btnEncrypt.Size = new Size(112, 34);
            btnEncrypt.TabIndex = 2;
            btnEncrypt.Text = "Encrypt";
            btnEncrypt.UseVisualStyleBackColor = true;
            btnEncrypt.Click += btnEncrypt_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 131);
            label2.Name = "label2";
            label2.Size = new Size(126, 25);
            label2.TabIndex = 3;
            label2.Text = "Encrypted Text";
            // 
            // txtEncryptedText
            // 
            txtEncryptedText.Location = new Point(177, 131);
            txtEncryptedText.Name = "txtEncryptedText";
            txtEncryptedText.Size = new Size(1067, 31);
            txtEncryptedText.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1430, 469);
            Controls.Add(txtEncryptedText);
            Controls.Add(label2);
            Controls.Add(btnEncrypt);
            Controls.Add(txtPlainText);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtPlainText;
        private Button btnEncrypt;
        private Label label2;
        private TextBox txtEncryptedText;
    }
}
