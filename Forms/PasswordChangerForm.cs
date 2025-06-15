using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project_addition_app.Models;
using project_addition_app.Forms;
namespace project_addition_app.Forms
{
    public partial class PasswordChangerForm : Form
    {
        AdisyonDBEntities context = new AdisyonDBEntities();
        private User m_user;
        public PasswordChangerForm(User user)
        {
            InitializeComponent();
            m_user = user;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SecurityHelper.CheckPasswordHash(old_pass.Text, m_user.SifreHash)) 
            {
                m_user.SifreHash = SecurityHelper.HashPassword(new_pass.Text);
                context.SaveChanges();
                MessageBox.Show("Şifreniz Başarıyla Değiştirildi!");
            
            }
            else
            {
                MessageBox.Show("Güncel şifrenizi yanlış girdiniz!");
            }
        }

        private void PasswordChangerForm_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.SaddleBrown;
        }
    }
}
