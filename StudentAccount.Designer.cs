namespace syncademia
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            label1 = new System.Windows.Forms.Label();
            textBox1 = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            guna2AnimateWindow1 = new Guna.UI2.WinForms.Guna2AnimateWindow(components);
            label2 = new System.Windows.Forms.Label();
            textBox2 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            textBox3 = new System.Windows.Forms.TextBox();
            label6 = new System.Windows.Forms.Label();
            textBox4 = new System.Windows.Forms.TextBox();
            label7 = new System.Windows.Forms.Label();
            textBox5 = new System.Windows.Forms.TextBox();
            label5 = new System.Windows.Forms.Label();
            textBox6 = new System.Windows.Forms.TextBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            // New controls for Phone and Percentage
            label8 = new System.Windows.Forms.Label();
            textBox7 = new System.Windows.Forms.TextBox();
            label9 = new System.Windows.Forms.Label();
            textBox8 = new System.Windows.Forms.TextBox();
            // Back button (rectangle with curved edges)
            btnBack = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();

            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            label1.ForeColor = System.Drawing.Color.RoyalBlue;
            label1.Location = new System.Drawing.Point(232, 328);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(79, 28);
            label1.TabIndex = 0;
            label1.Text = "Name :";
            label1.Click += label1_Click;
            // 
            // textBox1
            // 
            textBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            textBox1.Location = new System.Drawing.Point(337, 328);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(253, 31);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Segoe UI", 19.8000011F, System.Drawing.FontStyle.Bold);
            label3.ForeColor = System.Drawing.Color.RoyalBlue;
            label3.Location = new System.Drawing.Point(296, 256);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(280, 46);
            label3.TabIndex = 4;
            label3.Text = "Student account";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label2.Location = new System.Drawing.Point(270, 377);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(41, 25);
            label2.TabIndex = 6;
            label2.Text = "ID :";
            label2.Click += label2_Click;
            // 
            // textBox2
            // 
            textBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox2.Location = new System.Drawing.Point(337, 371);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(250, 31);
            textBox2.TabIndex = 7;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label4.Location = new System.Drawing.Point(204, 418);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(107, 25);
            label4.TabIndex = 10;
            label4.Text = "Username :";
            // 
            // textBox3
            // 
            textBox3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox3.Location = new System.Drawing.Point(337, 412);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new System.Drawing.Size(250, 31);
            textBox3.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label6.Location = new System.Drawing.Point(243, 467);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(68, 25);
            label6.TabIndex = 16;
            label6.Text = "Email :";
            // 
            // textBox4
            // 
            textBox4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox4.Location = new System.Drawing.Point(337, 461);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new System.Drawing.Size(253, 31);
            textBox4.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label7.Location = new System.Drawing.Point(252, 509);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(59, 25);
            label7.TabIndex = 19;
            label7.Text = "Year :";
            // 
            // textBox5
            // 
            textBox5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox5.Location = new System.Drawing.Point(337, 509);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(253, 31);
            textBox5.TabIndex = 20;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label5.Location = new System.Drawing.Point(211, 565);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(100, 25);
            label5.TabIndex = 22;
            label5.Text = "Major :";
            // 
            // textBox6
            // 
            textBox6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox6.Location = new System.Drawing.Point(337, 559);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(250, 31);
            textBox6.TabIndex = 23;
            textBox6.Text = " ";
            // 
            // label8 (Phone)
            // 
            label8.AutoSize = true;
            label8.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label8.ForeColor = System.Drawing.Color.RoyalBlue;
            label8.Location = new System.Drawing.Point(211, 610);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(68, 25);
            label8.TabIndex = 100;
            label8.Text = "Phone :";
            // 
            // textBox7 (Phone)
            // 
            textBox7.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox7.Location = new System.Drawing.Point(337, 604);
            textBox7.Name = "textBox7";
            textBox7.ReadOnly = true;
            textBox7.Size = new System.Drawing.Size(253, 31);
            textBox7.TabIndex = 101;
            // 
            // label9 (Percentage)
            // 
            label9.AutoSize = true;
            label9.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold);
            label9.ForeColor = System.Drawing.Color.RoyalBlue;
            label9.Location = new System.Drawing.Point(211, 660);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(105, 25);
            label9.TabIndex = 102;
            label9.Text = "Percentage :";
            // 
            // textBox8 (Percentage)
            // 
            textBox8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            textBox8.Location = new System.Drawing.Point(337, 654);
            textBox8.Name = "textBox8";
            textBox8.ReadOnly = true;
            textBox8.Size = new System.Drawing.Size(253, 31);
            textBox8.TabIndex = 103;
            // 
            // btnBack (Rectangle with curved edges)
            // 
            btnBack.BorderRadius = 12;
            btnBack.BorderThickness = 0;
            btnBack.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            btnBack.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            btnBack.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
            btnBack.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
            btnBack.FillColor = System.Drawing.Color.RoyalBlue;
            btnBack.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnBack.ForeColor = System.Drawing.Color.White;
            btnBack.Location = new System.Drawing.Point(379, 718);
            btnBack.Name = "btnBack";
            btnBack.Size = new System.Drawing.Size(115, 44);
            btnBack.TabIndex = 24;
            btnBack.Text = "Back";
            btnBack.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = System.Drawing.Color.Transparent;
            pictureBox1.Location = new System.Drawing.Point(296, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(232, 183);
            pictureBox1.TabIndex = 25;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 800);
            this.Controls.Add(textBox8);
            this.Controls.Add(label9);
            this.Controls.Add(textBox7);
            this.Controls.Add(label8);
            this.Controls.Add(pictureBox1);
            this.Controls.Add(btnBack);
            this.Controls.Add(textBox6);
            this.Controls.Add(label5);
            this.Controls.Add(textBox5);
            this.Controls.Add(label7);
            this.Controls.Add(textBox4);
            this.Controls.Add(label6);
            this.Controls.Add(textBox3);
            this.Controls.Add(textBox2);
            this.Controls.Add(label4);
            this.Controls.Add(label2);
            this.Controls.Add(label3);
            this.Controls.Add(textBox1);
            this.Controls.Add(label1);
            this.ForeColor = System.Drawing.Color.RoyalBlue;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "SYNCADEMIA";
            this.Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Existing variables plus new ones
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2AnimateWindow guna2AnimateWindow1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.PictureBox pictureBox1;
        // New
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox8;
        // Back button
        private Guna.UI2.WinForms.Guna2Button btnBack;
    }
}