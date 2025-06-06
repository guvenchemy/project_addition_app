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
    AdisyonDBEntities context = new AdisyonDBEntities();
    public partial class PasswordChangerForm : Form
    {
        private User m_user;
        public PasswordChangerForm(User user)
        {
            InitializeComponent();
            m_user = user;
        }
    }
}
