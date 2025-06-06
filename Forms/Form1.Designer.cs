namespace project_addition_app
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.name_textBox1 = new System.Windows.Forms.TextBox();
            this.password_textBox2 = new System.Windows.Forms.TextBox();
            this.girisYap_button = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.resimYukle_button = new System.Windows.Forms.Button();
            this.name_textBox4 = new System.Windows.Forms.TextBox();
            this.nickname_textBox3 = new System.Windows.Forms.TextBox();
            this.kayitOl_button = new System.Windows.Forms.Button();
            this.password_textBox5 = new System.Windows.Forms.TextBox();
            this.role_comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // name_textBox1
            // 
            resources.ApplyResources(this.name_textBox1, "name_textBox1");
            this.name_textBox1.Name = "name_textBox1";
            // 
            // password_textBox2
            // 
            resources.ApplyResources(this.password_textBox2, "password_textBox2");
            this.password_textBox2.Name = "password_textBox2";
            // 
            // girisYap_button
            // 
            resources.ApplyResources(this.girisYap_button, "girisYap_button");
            this.girisYap_button.Name = "girisYap_button";
            this.girisYap_button.UseVisualStyleBackColor = true;
            this.girisYap_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightCoral;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.resimYukle_button);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.LavenderBlush;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // resimYukle_button
            // 
            resources.ApplyResources(this.resimYukle_button, "resimYukle_button");
            this.resimYukle_button.Name = "resimYukle_button";
            this.resimYukle_button.UseVisualStyleBackColor = true;
            this.resimYukle_button.Click += new System.EventHandler(this.resimYukle_button_Click);
            // 
            // name_textBox4
            // 
            resources.ApplyResources(this.name_textBox4, "name_textBox4");
            this.name_textBox4.Name = "name_textBox4";
            // 
            // nickname_textBox3
            // 
            resources.ApplyResources(this.nickname_textBox3, "nickname_textBox3");
            this.nickname_textBox3.Name = "nickname_textBox3";
            // 
            // kayitOl_button
            // 
            resources.ApplyResources(this.kayitOl_button, "kayitOl_button");
            this.kayitOl_button.Name = "kayitOl_button";
            this.kayitOl_button.UseVisualStyleBackColor = true;
            this.kayitOl_button.Click += new System.EventHandler(this.kayitOl_button_Click);
            // 
            // password_textBox5
            // 
            resources.ApplyResources(this.password_textBox5, "password_textBox5");
            this.password_textBox5.Name = "password_textBox5";
            // 
            // role_comboBox1
            // 
            this.role_comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.role_comboBox1, "role_comboBox1");
            this.role_comboBox1.Name = "role_comboBox1";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.girisYap_button);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.password_textBox2);
            this.panel2.Controls.Add(this.name_textBox1);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.kayitOl_button);
            this.panel3.Controls.Add(this.password_textBox5);
            this.panel3.Controls.Add(this.name_textBox4);
            this.panel3.Controls.Add(this.nickname_textBox3);
            this.panel3.Controls.Add(this.role_comboBox1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.panel1);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCoral;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox name_textBox1;
        private System.Windows.Forms.TextBox password_textBox2;
        private System.Windows.Forms.Button girisYap_button;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox name_textBox4;
        private System.Windows.Forms.TextBox nickname_textBox3;
        private System.Windows.Forms.Button kayitOl_button;
        private System.Windows.Forms.TextBox password_textBox5;
        private System.Windows.Forms.ComboBox role_comboBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button resimYukle_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label9;
    }
}

