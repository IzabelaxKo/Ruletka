namespace Ruletka
{
    partial class Register
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
            this.label1 = new System.Windows.Forms.Label();
            this.UserName = new System.Windows.Forms.TextBox();
            this.Pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.RepeatPass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nameError = new System.Windows.Forms.Label();
            this.PasswordError = new System.Windows.Forms.Label();
            this.RepeatPasswordError = new System.Windows.Forms.Label();
            this.roundedButton1 = new RoundedButton();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(34, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nazwa użytkownika";
            // 
            // UserName
            // 
            this.UserName.Location = new System.Drawing.Point(166, 30);
            this.UserName.Name = "UserName";
            this.UserName.Size = new System.Drawing.Size(162, 20);
            this.UserName.TabIndex = 1;
            // 
            // Pass
            // 
            this.Pass.Location = new System.Drawing.Point(166, 90);
            this.Pass.Name = "Pass";
            this.Pass.PasswordChar = '*';
            this.Pass.Size = new System.Drawing.Size(162, 20);
            this.Pass.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(34, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hasło";
            // 
            // RepeatPass
            // 
            this.RepeatPass.Location = new System.Drawing.Point(166, 150);
            this.RepeatPass.Name = "RepeatPass";
            this.RepeatPass.PasswordChar = '*';
            this.RepeatPass.Size = new System.Drawing.Size(162, 20);
            this.RepeatPass.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(34, 150);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Powtórz hasło";
            // 
            // nameError
            // 
            this.nameError.AutoSize = true;
            this.nameError.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.nameError.ForeColor = System.Drawing.Color.Red;
            this.nameError.Location = new System.Drawing.Point(164, 53);
            this.nameError.Name = "nameError";
            this.nameError.Size = new System.Drawing.Size(171, 12);
            this.nameError.TabIndex = 6;
            this.nameError.Text = "Nazwa użytkownika nie możę byc pusta";
            // 
            // PasswordError
            // 
            this.PasswordError.AutoSize = true;
            this.PasswordError.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.PasswordError.ForeColor = System.Drawing.Color.Red;
            this.PasswordError.Location = new System.Drawing.Point(164, 113);
            this.PasswordError.Name = "PasswordError";
            this.PasswordError.Size = new System.Drawing.Size(110, 12);
            this.PasswordError.TabIndex = 7;
            this.PasswordError.Text = "Hasło nie może byc puste";
            // 
            // RepeatPasswordError
            // 
            this.RepeatPasswordError.AutoSize = true;
            this.RepeatPasswordError.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.RepeatPasswordError.ForeColor = System.Drawing.Color.Red;
            this.RepeatPasswordError.Location = new System.Drawing.Point(164, 173);
            this.RepeatPasswordError.Name = "RepeatPasswordError";
            this.RepeatPasswordError.Size = new System.Drawing.Size(64, 12);
            this.RepeatPasswordError.TabIndex = 8;
            this.RepeatPasswordError.Text = "Powtórz hasło";
            // 
            // roundedButton1
            // 
            this.roundedButton1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.roundedButton1.BorderRadius = 20;
            this.roundedButton1.ForeColor = System.Drawing.Color.White;
            this.roundedButton1.Location = new System.Drawing.Point(105, 190);
            this.roundedButton1.Name = "roundedButton1";
            this.roundedButton1.Size = new System.Drawing.Size(150, 50);
            this.roundedButton1.TabIndex = 12;
            this.roundedButton1.Text = "Zarejestruj";
            this.roundedButton1.UseVisualStyleBackColor = false;
            this.roundedButton1.Click += new System.EventHandler(this.roundedButton1_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4.Location = new System.Drawing.Point(97, 243);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "Masz już konto? Zarejstruj się teraz!";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 266);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.roundedButton1);
            this.Controls.Add(this.RepeatPasswordError);
            this.Controls.Add(this.PasswordError);
            this.Controls.Add(this.nameError);
            this.Controls.Add(this.RepeatPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Pass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.UserName);
            this.Controls.Add(this.label1);
            this.Name = "Register";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UserName;
        private System.Windows.Forms.TextBox Pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox RepeatPass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label nameError;
        private System.Windows.Forms.Label PasswordError;
        private System.Windows.Forms.Label RepeatPasswordError;
        private RoundedButton roundedButton1;
        private System.Windows.Forms.Label label4;
    }
}