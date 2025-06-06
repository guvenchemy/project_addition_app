using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_addition_app.Forms;
using project_addition_app.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace project_addition_app
{
    public partial class Form1 : Form
    {
        AdisyonDBEntities context = new AdisyonDBEntities();
        string ProfilePicName = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            role_comboBox1.DataSource = context.Roles.ToList();
            role_comboBox1.ValueMember = "Id";
            role_comboBox1.DisplayMember = "RolAdi";
            role_comboBox1.Text = "Seçiniz";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var kullanici = context.Users.FirstOrDefault(k => k.KullaniciAdi == name_textBox1.Text);
                if (kullanici != null && SecurityHelper.CheckPasswordHash(password_textBox2.Text, kullanici.SifreHash))
                {
                    MessageBox.Show("Giriş Başarılı.");
                    MainForm main = new MainForm(kullanici);
                    this.Hide();
                    main.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resimYukle_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileOpen.Title = "Open Image file";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(fileOpen.FileName);
                string picture_name = fileOpen.FileName;
                string picture_ext = Path.GetExtension(fileOpen.FileName);

                string folder = @"images\";
                string random_name = Guid.NewGuid().ToString() + picture_ext;
                var path = Path.Combine(folder, random_name);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                System.IO.File.Copy(picture_name, path);
                string ProfilePicName2 = random_name;
                ProfilePicName = random_name;

            }
            fileOpen.Dispose();
        }

        private void kayitOl_button_Click(object sender, EventArgs e)
        {
            string passwordHash = SecurityHelper.HashPassword(password_textBox5.Text);
            try
            {
                if (context.Users.Any(x => x.KullaniciAdi == nickname_textBox3.Text))
                    throw new Exception("bu kullanıcı adı daha önce kullanılmış");
                if (string.IsNullOrWhiteSpace(name_textBox4.Text))
                    throw new Exception("Ad Soyad boş olamaz.");
                if (string.IsNullOrWhiteSpace(nickname_textBox3.Text))
                    throw new Exception("Kullanıcı adı boş olamaz.");
                if (string.IsNullOrWhiteSpace(password_textBox5.Text))
                    throw new Exception("Şifre boş olamaz.");
                if (role_comboBox1.Text == "Seçiniz")
                    throw new Exception("Rol seçiniz.");
                if (string.IsNullOrWhiteSpace(ProfilePicName))
                    throw new Exception("Profil resmi yükleyiniz.");
                User new_user = new User();
                new_user.AdSoyad = name_textBox4.Text;
                new_user.KullaniciAdi = nickname_textBox3.Text;
                new_user.SifreHash = passwordHash;
                new_user.RoleId = (int)role_comboBox1.SelectedValue;
                if (ProfilePicName != string.Empty)
                {
                    new_user.ProfilePicName = ProfilePicName;
                }
                if(context.Users.Any(x => x.RoleId == 1))
                {
                    if (!((int)role_comboBox1.SelectedValue == (int)context.Roles.FirstOrDefault(x => x.Id == 1).Id))
                    {
                        context.Users.Add(new_user);
                        context.SaveChanges();
                        MessageBox.Show("Kayıt Başarılı...");
                        MainForm main = new MainForm(new_user);
                        this.Hide();
                        main.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Admin rolü için kayıt olamazsınız");
                        return;
                    }
                }
                else
                {
                    context.Users.Add(new_user);
                    context.SaveChanges();
                    MessageBox.Show("Kayıt Başarılı...");
                    MainForm main = new MainForm(new_user);
                    this.Hide();
                    main.ShowDialog();
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Hata: {ex}");

            }

            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = true;
        }
    }
}
