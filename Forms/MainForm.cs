using iTextSharp.text;
using iTextSharp.text.pdf;
using project_addition_app.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project_addition_app.Forms
{
    public partial class MainForm : Form
    {
        AdisyonDBEntities context = new AdisyonDBEntities();
        User m_user;
        Order m_order = null;
        User m_user_to_change;
        Order m_order_ = null;
        Table m_table = null;
        OrderDetail m_order_detail = null;
        int SelectedProductId = 0;
        int SelectedCategoryId = 0;
        string ProfilePicName;
        string role;
        List<Table> tables0;
        public MainForm(User user)
        {
            InitializeComponent();
            m_user = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadTables();
            ResizeButtons();
            ListAllOrders();
            ListRoles();
            tables0 = context.Tables.ToList();
            tables0.Insert(0, new Table { Id = 0, MasaNumarasi = "Seçiniz" });
            table_num_comboBox1.DataSource = tables0;
            table_num_comboBox1.DisplayMember = "MasaNumarasi";
            table_num_comboBox1.ValueMember = "Id";
            var payment_types = context.PaymentTypes.ToList();
            payment_types.Insert(0, new PaymentType { Id = 0, OdemeTuru = "Seçiniz" });
            payment_comboBox1.DataSource = payment_types;
            payment_comboBox1.DisplayMember = "OdemeTuru";
            payment_comboBox1.ValueMember = "Id";
            payment_dataGridView1.DataSource = context.AktifSiparisler_VV.ToList();
            ListPayments();

            var categories = context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, KategoriAdi = "Seçiniz" });

            welcome_label.Text = "Hoşgeldiniz " + m_user.AdSoyad;
            role = context.Roles.Find(m_user.RoleId).RolAdi.ToString();
            role_label.Text = role;
            profile_picBox.Image = System.Drawing.Image.FromFile(@"images\" + m_user.ProfilePicName);

            if (m_table == null)
            {
                delete_table.Enabled = false;
                update_table_num.Enabled = false;
            }
            Listele();
            total_label.Visible = false;
            order_num_label.Visible = false;
            var tables = context.Tables.ToList();
            tables.Insert(0, new Table { Id = 0, MasaNumarasi = "Seçiniz" });
            role_comboBox1.DataSource = context.Roles.ToList();
            role_comboBox1.ValueMember = "Id";
            role_comboBox1.DisplayMember = "RolAdi";
            role_comboBox1.Text = "Seçiniz";

        }
        public void Listele()
        {
            var list = context.AktifSiparisler_VV.OrderByDescending(x => x.Sipariş_Numarası).ToList();

            ordersDGW.DataSource = list;
        }
        public void ListAllOrders()
        {
            allOrders_dataGridView1.DataSource = context.SiparisListesi_V.ToList();
        }
        public void ListPayments()
        {

            payments_dataGridView1.DataSource = context.Payment_V.ToList();
            payments_dataGridView1.Columns["Payment_Id"].Visible = false;
            payments_dataGridView1.Columns["OrderId"].HeaderText = "Sipariş Numarası";
            payment_dataGridView1.DataSource = context.AktifSiparisler_VV.ToList();

        }



        public void FillTablesList(dynamic control, string TableNum = "")
        {
            var table_list = context.Tables.ToList();
            table_list.Insert(0, new Table { Id = 0, MasaNumarasi = "Seçiniz" });
            control.DataSource = table_list;
            control.DisplayMember = "MasaNumarasi";
            control.ValueMember = "Id";
            if (TableNum == "")
            {
                control.Text = "Seçiniz";

            }
            else
            {
                control.SelectedIndex = control.FindStringExact(TableNum);
            }
        }
        public void FillCategoryList(dynamic control, string CategoryName = "")
        {
            var categoryList = context.Categories.ToList();
            control.DataSource = categoryList;
            control.DisplayMember = "KategoriAdi";
            control.ValueMember = "Id";
            if (CategoryName == "")
            {
                control.Text = "Seçiniz";
            }
            else
            {
                control.SelectedIndex = control.FindStringExact(CategoryName);
            }

        }

        public void FillProductList(dynamic control, string ProductName = "")
        {
            var productList = context.Products.ToList();
            control.DataSource = productList;
            control.DisplayMember = "UrunAdi";
            control.ValueMember = "Id";
            if (ProductName == "")
            {
                control.Text = "Seçiniz";
            }
            else
            {
                control.SelectedIndex = control.FindStringExact(ProductName);
            }

        }
        public void FillRolesList(dynamic control, string RoleName = "")
        {
            var rolesList = context.Roles.ToList();
            control.DataSource = rolesList;
            control.DisplayMember = "RolAdi";
            control.ValueMember = "Id";
            if (RoleName == "")
            {
                control.text = "Seçiniz";
            }
            else
            {
                control.SelectedIndex = control.FindStringExact(RoleName);
            }
        }


        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (role != "Admin")
            {
                if (e.TabPage == adminPanel)
                {
                    e.Cancel = true;
                }
            }
            if (e.TabPage == tableManagement)
            {
                ListTables();
            }
            if (e.TabPage == welcome_page)
            {

                ListAllOrders();
            }
            if (e.TabPage == productManagement)
            {
                FillCategoryCMB(catergoryCMB);
                LoadProductData();
            }
        }
        void FillCategoryCMB(ComboBox control, string CategoryName = "")
        {
            var categories = context.Categories.ToList();
            categories.Insert(0, new Category { Id = 0, KategoriAdi = "Seçiniz" });
            control.DataSource = categories;
            control.DisplayMember = "KategoriAdi";
            control.ValueMember = "Id";
            if (CategoryName == "")
            {
                control.Text = "Seçiniz";
            }
            else
            {
                control.SelectedIndex = control.FindStringExact(CategoryName);
            }
        }
        public void LoadProductData(int categoryId = 0)
        {
            List<ProductsCategory> products;
            List<Category> categories = context.Categories.ToList();
            categoriesDGV.DataSource = categories;
            categoriesDGV.Columns["Products"].Visible = false;
            if (categoryId != 0)
            {
                products = context.ProductsCategories.Where(p => p.CategoryId == categoryId).ToList();
                
            }
            else
            {
                products = context.ProductsCategories.ToList();
            }
            productsDGV.DataSource = products;
            productsDGV.Columns["Id"].Visible = false;
            productsDGV.Columns["CategoryId"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Table new_table = new Table()
            {
                MasaNumarasi = table_number_textBox1.Text,
                Durum = false,
            };
            if (context.Tables.Any(t => t.MasaNumarasi == new_table.MasaNumarasi))
            {
                MessageBox.Show("Bu masa zaten var, lütfen başka bir masa numarası giriniz...");
            }
            else
            {
                context.Tables.Add(new_table);
                context.SaveChanges();
                ListTables();

            }
        }

        public void ListTables()
        {
            var tables = context.Tables.ToList();
            tables_dataGridView1.DataSource = tables;
            tables_dataGridView1.Columns["Id"].Visible = false;
            tables_dataGridView1.Columns["Orders"].Visible = false;
        }

        private void tables_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                var table_id = tables_dataGridView1.Rows[rowIndex].Cells["Id"].Value;
                //MessageBox.Show("Deger" + table_id); // for debugging
                m_table = context.Tables.Find(table_id);
                table_number_textBox1.Text = m_table.MasaNumarasi.ToString();
                if (m_table != null)
                {
                    delete_table.Enabled = true;
                    update_table_num.Enabled = true;
                }
            }

        }
        void ListRoles()
        {
            users_dataGridView1.DataSource = context.Users_V.ToList();
            users_dataGridView1.Columns["Id"].Visible = false;
        }
        private void update_table_num_Click(object sender, EventArgs e)
        {
            bool durum = context.Tables.Any(t => t.MasaNumarasi == table_number_textBox1.Text && t.Id != m_table.Id);
            if (durum)
            {
                MessageBox.Show("Bu masa numarası zaten var, lütfen başka bir masa numarası giriniz...");
                return;
            }
            else
            {
                m_table.MasaNumarasi = table_number_textBox1.Text;
                context.SaveChanges();
                ListTables();

            }
        }

        private void delete_table_Click(object sender, EventArgs e)
        {
            context.Tables.Remove(m_table);
            context.SaveChanges();
            ListTables();
            delete_table.Enabled = false;
        }

        private void users_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                var user_id = users_dataGridView1.Rows[rowIndex].Cells["Id"].Value;
                MessageBox.Show("Deger" + user_id); // for debugging
                m_user_to_change = context.Users.Find(user_id);
                user_textBox1.Text = m_user_to_change.KullaniciAdi.ToString();
                fullname_textBox1.Text = m_user_to_change.AdSoyad.ToString();
                pp_pictureBox1.Image = System.Drawing.Image.FromFile(@"images\" + m_user_to_change.ProfilePicName);
                ProfilePicName = m_user_to_change.ProfilePicName;
                pp_pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                FillRolesList(role_comboBox1, m_user_to_change.Role.RolAdi);

            }
        }

        private void update_user_Click(object sender, EventArgs e)
        {
            if (context.Users.Any(u => u.KullaniciAdi == user_textBox1.Text && m_user_to_change.KullaniciAdi != user_textBox1.Text))
            {
                MessageBox.Show("Bu kullanıcı adı zaten kullanılmakta!");
            }
            else
            {
                m_user_to_change.AdSoyad = fullname_textBox1.Text;
                m_user_to_change.RoleId = (int)role_comboBox1.SelectedValue;
                m_user_to_change.KullaniciAdi = user_textBox1.Text;
                if (ProfilePicName != string.Empty)
                {
                    m_user_to_change.ProfilePicName = ProfilePicName;
                }
                if (m_user_to_change.Id == m_user.Id)
                {
                    m_user = m_user_to_change;
                    profile_picBox.Image = System.Drawing.Image.FromFile(@"images\" + m_user.ProfilePicName);
                }
                context.SaveChanges();
                ListRoles();
            }
        }

        private void exit_button1_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            var result = MessageBox.Show("Çıkmak istediğinizden emin misiniz?", "Çıkış", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void delete_user_Click(object sender, EventArgs e)
        {
            if (m_user_to_change == m_user)
            {
                context.Users.Remove(m_user_to_change);
                Exit();
            }
            else
            {
                context.Users.Remove(m_user_to_change);
            }
            ListRoles();
        }

        private void min_textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void max_textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void upload_picture_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileOpen.Title = "Open Image file";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                pp_pictureBox1.Image = System.Drawing.Image.FromFile(fileOpen.FileName);
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

        private void filter_button_Click_1(object sender, EventArgs e)
        {
            var orders = context.SiparisListesi_V.ToList();
            if (min_textBox1.Text.Length > 0)
            {
                orders = orders.Where(o => o.Hesap_Toplam_Tutar >= decimal.Parse(min_textBox1.Text)).ToList();
            }
            if (max_textBox2.Text.Length > 0)
            {
                orders = orders.Where(o => o.Hesap_Toplam_Tutar <= decimal.Parse(max_textBox2.Text)).ToList();
            }
            if (table_num_comboBox1.SelectedIndex != 0)
            {
                var table = tables0.FirstOrDefault(t => t.Id == (int)table_num_comboBox1.SelectedValue);
                orders = orders.Where(o => o.Masa_Numarası == table.MasaNumarasi).ToList();
            }
            if (min_dateTimePicker1.Value.Date != DateTime.Today)
                orders = orders.Where(o => o.Tarih >= min_dateTimePicker1.Value.Date).ToList();

            if (max_dateTimePicker2.Value.Date != DateTime.Today)
                orders = orders.Where(o => o.Tarih <= max_dateTimePicker2.Value.Date).ToList();
            if (checkBox1.Checked == true)
            {
                orders = orders.Where(o => o.Durum == true).ToList();
            }
            else
            {
                orders = orders.Where(o => o.Durum == false).ToList();
            }

            allOrders_dataGridView1.DataSource = orders;
        }

        private void remove_filters_Click(object sender, EventArgs e)
        {
            allOrders_dataGridView1.DataSource = context.SiparisListesi_V.ToList();
        }

        private void payment_dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            if (rowIndex >= 0)
            {
                var order_id = payment_dataGridView1.Rows[rowIndex].Cells["Sipariş_Numarası"].Value;
                m_order = context.Orders.Find(order_id);

                total_label.Text = m_order.ToplamTutar.ToString();
                order_num_label.Text = m_order.Id.ToString();
                total_label.Visible = true;
                order_num_label.Visible = true;

            }
        }

        private void pay_button_Click(object sender, EventArgs e)
        {
            if (payment_comboBox1.Text != "Seçiniz" && m_order != null)
            {
                Payment payment = new Payment();
                payment.OrderId = m_order.Id;
                payment.UserId = m_user.Id;
                payment.Tutar = m_order.ToplamTutar;
                payment.PaymentTypeId = (int)payment_comboBox1.SelectedValue;
                payment.Tarih = DateTime.Now;
                payment.Aciklama = description_textBox1.Text;
                m_order.Durum = false;
                m_order.Table.Durum = false;
                context.Payments.Add(payment);
                context.SaveChanges();
                ListPayments();
                Listele();
                LoadTables();
                ResizeButtons();
                payment_dataGridView1.DataSource = context.AktifSiparisler_VV.ToList();

            }
            else if (m_order == null)
            {
                MessageBox.Show("Lütfen sipariş seçip tekrar deneyiniz...");
                return;
            }
            else
            {
                MessageBox.Show("Lütfen ödeme tipini seçip tekrar deneyiniz...");
                return;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog fileOpen = new OpenFileDialog();
            fileOpen.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileOpen.Title = "Open Image file";

            if (fileOpen.ShowDialog() == DialogResult.OK)
            {
                profile_picBox.Image = System.Drawing.Image.FromFile(fileOpen.FileName);
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


            m_user.ProfilePicName = ProfilePicName;
            context.SaveChanges();
        }

        private void pdf_button_Click(object sender, EventArgs e)
        {
            List<string> gizlenecekSutunlar = new List<string> { "Masa_Numarası" };
            // PDF belgesi oluşturuluyor
            Document doc = new Document();
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.pdf");
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(outputPath, FileMode.Create));

            doc.Open();

            // Görünen sütun sayısına göre tablo oluştur
            int gorunenSutunSayisi = 0;
            foreach (DataGridViewColumn column in (allOrders_dataGridView1.Columns))
            {
                if (!gizlenecekSutunlar.Contains(column.HeaderText))
                    gorunenSutunSayisi++;
            }

            PdfPTable table = new PdfPTable(gorunenSutunSayisi);

            // Sütun başlıklarını ekle (filtreleyerek)
            foreach (DataGridViewColumn column in (allOrders_dataGridView1.Columns))
            {
                if (!gizlenecekSutunlar.Contains(column.HeaderText))
                {
                    table.AddCell(new PdfPCell(new Phrase(column.HeaderText)));
                }
            }

            // Satır verilerini ekle
            foreach (DataGridViewRow row in (allOrders_dataGridView1.Rows))
            {
                if (!row.IsNewRow)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (!gizlenecekSutunlar.Contains(cell.OwningColumn.HeaderText))
                        {
                            table.AddCell(cell.Value?.ToString() ?? string.Empty);
                        }
                    }
                }
            }

            // Tabloyu PDF'e ekle ve kapat
            doc.Add(table);
            doc.Close();
            writer.Close();

            // Bilgilendirme ve PDF dosyasını açma
            MessageBox.Show("PDF başarıyla oluşturuldu! Dosya konumu: " + outputPath);
            Process.Start(new ProcessStartInfo(outputPath) { UseShellExecute = true });

        }
        //ORDER MANAGEMENT//

        public void LoadTables()
        {
            flowLayoutPanel1.Controls.Clear();
            var tables = context.Tables.ToList();
            foreach (var table in tables)
            {
                Button btn = new Button();
                btn.Text = table.MasaNumarasi;
                btn.Tag = table;
                btn.Enabled = table.Durum ? false : true;
                btn.Click += (s, e) =>
                {
                    if (btn.Tag != null)
                    {
                        OrderForm of = new OrderForm(table);
                        of.ShowDialog();
                        btn.Enabled = false;
                        table.Durum = true;
                        context.SaveChanges();
                        btn.Tag = table;
                    }
                    LoadTables();
                    ResizeButtons();
                    Listele();
                };
                flowLayoutPanel1.Controls.Add(btn);
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResizeButtons();
        }

        public void ResizeButtons()
        {
            int butonSayisiYanYana = 3; // Varsayılan olarak yan yana kaç buton olsun istiyorsan
            int padding = 12;

            // Her butonun genişliği = Panelin genişliği / buton sayısı - boşluk
            int butonGenislik = (flowLayoutPanel1.Width / butonSayisiYanYana) - padding;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = butonGenislik;
                    btn.Height = (int)(butonGenislik * 0.8); // Yüksekliği orantılı istersen
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListPayments();
            ListTables();
            ListAllOrders();
            Listele();
            LoadTables();
            ResizeButtons();
        }

        private void ordersDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 0)
                {
                    try
                    {
                        var orderId = ordersDGW.Rows[e.RowIndex].Cells["Sipariş_Numarası"].Value;
                        var order = context.Orders.Find(orderId);
                        var details = context.OrderDetails.Where(od => od.OrderId == (int)orderId);
                        var table = context.Tables.Find(order.TableId);
                        order.Durum = false;
                        table.Durum = false;
                        foreach (var detail in details)
                        {
                            context.OrderDetails.Remove(detail);
                        }
                        context.Orders.Remove(order);
                        context.SaveChanges();
                        ListPayments();
                        ListTables();
                        ListAllOrders();
                        Listele();
                        LoadTables();
                        ResizeButtons();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Hata: " + ex);
                    }
                }
                else
                {
                    var orderId = ordersDGW.Rows[e.RowIndex].Cells["Sipariş_Numarası"].Value;
                    m_order = context.Orders.Find(orderId);
                    OrderForm of = new OrderForm(m_order.Table, m_order);
                    of.ShowDialog();
                }
            }
        }

        private void catergoryCMB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (catergoryCMB.SelectedIndex != 0)
            {
                var catId = (int)catergoryCMB.SelectedValue;
                LoadProductData(catId);
            }
            else
            {
                LoadProductData();
            }
        }

        private void saveProduct_Click(object sender, EventArgs e)
        {
            if (productNAME.Text == string.Empty || productPRICE.Text == string.Empty || catergoryCMB.Text == "Seçiniz")
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz!");
                return;
            }
            else
            {
                bool durum = context.Products.Any(p => p.UrunAdi.ToLower() == productNAME.Text.ToLower());
                if (SelectedProductId == 0)
                {

                    if (!durum)
                    {
                        Product product = new Product();
                        product.UrunAdi = productNAME.Text;
                        if (!decimal.TryParse(productPRICE.Text, out decimal price))
                        {
                            MessageBox.Show("Geçerli bir fiyat giriniz!");
                            return;
                        }
                        else
                        {
                            product.Fiyat = decimal.Parse(productPRICE.Text);

                        }
                        product.CategoryId = (int)catergoryCMB.SelectedValue;

                        context.Products.Add(product);
                        context.SaveChanges();
                        MessageBox.Show("Ürün başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu ürün zaten mevcut, lütfen başka bir ürün adı giriniz.");
                        return;
                    }

                }
                else
                {
                    if (!durum)
                    {
                        var product = context.Products.Find(SelectedProductId);
                        product.UrunAdi = productNAME.Text;
                        if (!decimal.TryParse(productPRICE.Text, out decimal price))
                        {
                            MessageBox.Show("Geçerli bir fiyat giriniz!");
                            return;
                        }
                        else
                        {
                            product.Fiyat = decimal.Parse(productPRICE.Text);

                        }
                        product.CategoryId = (int)catergoryCMB.SelectedValue;
                        context.SaveChanges();
                        MessageBox.Show("Ürün başarıyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu ürün zaten mevcut, lütfen başka bir ürün adı giriniz.");
                        return;
                    }
                }
                var catId = (int)catergoryCMB.SelectedValue;
                LoadProductData(catId);
                SelectedProductId = 0;
            }
        }

        private void productsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedProductId = Convert.ToInt32(productsDGV.Rows[e.RowIndex].Cells["Id"].Value);
                var product = context.Products.Find(SelectedProductId);
                catergoryCMB.SelectedValue = product.CategoryId;
                productNAME.Text = product.UrunAdi;
                productPRICE.Text = product.Fiyat.ToString();

            }
        }

        private void saveCategory_Click(object sender, EventArgs e)
        {
            if (categoryNAME.Text == string.Empty)
            {
                MessageBox.Show("Lütfen kategori adını giriniz!");
                return;
            }
            else
            {
                bool durum = context.Categories.Any(c => c.KategoriAdi.ToLower() == categoryNAME.Text.ToLower());
                if (SelectedCategoryId == 0)
                {
                    if (!durum)
                    {
                        Category category = new Category();
                        category.KategoriAdi = categoryNAME.Text;
                        context.Categories.Add(category);
                        context.SaveChanges();
                        MessageBox.Show("Kategori başarıyla eklendi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu kategori zaten mevcut, lütfen başka bir kategori adı giriniz.");
                        return;
                    }
                }
                else
                {
                    if (!durum)
                    {
                        var category = context.Categories.Find(SelectedCategoryId);
                        category.KategoriAdi = categoryNAME.Text;
                        context.SaveChanges();
                        MessageBox.Show("Kategori başarıyla güncellendi.");
                    }
                    else
                    {
                        MessageBox.Show("Bu kategori zaten mevcut, lütfen başka bir kategori adı giriniz.");
                        return;
                    }
                }
                FillCategoryCMB(catergoryCMB);
                LoadProductData();
                SelectedCategoryId = 0;
            }
        }

        private void categoriesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SelectedCategoryId = Convert.ToInt32(categoriesDGV.Rows[e.RowIndex].Cells["Id"].Value);
                var category = context.Categories.Find(SelectedCategoryId);
                categoryNAME.Text = category.KategoriAdi;
            }
        }

        private void deleteCategory_Click(object sender, EventArgs e)
        {
            if(SelectedCategoryId == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz kategoriyi seçiniz.");
                return;
            }
            else
            {
                var category = context.Categories.Find(SelectedCategoryId);
                if (category != null)
                {
                    var products = context.Products.Where(p => p.CategoryId == SelectedCategoryId).ToList();
                    foreach (var product in products)
                    {
                        context.Products.Remove(product);
                    }
                    context.Categories.Remove(category);
                    context.SaveChanges();
                    MessageBox.Show("Kategori başarıyla silindi.");
                    FillCategoryCMB(catergoryCMB);
                    LoadProductData();
                }
                else
                {
                    MessageBox.Show("Kategori bulunamadı.");
                    return;
                }
                SelectedCategoryId = 0;
            }
        }

        private void deleteProduct_Click(object sender, EventArgs e)
        {
            if(SelectedProductId == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz ürünü seçiniz.");
                return;
            }
            else
            {
                var product = context.Products.Find(SelectedProductId);
                if (product != null)
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    MessageBox.Show("Ürün başarıyla silindi.");
                    var catId = (int)catergoryCMB.SelectedValue;
                    LoadProductData(catId);
                }
                else
                {
                    MessageBox.Show("Ürün bulunamadı.");
                    return;
                }
                SelectedProductId = 0;
            }
        }
        // Sonra Tekrar Gel.
        private void changePass_Click(object sender, EventArgs e)
        {
            PasswordChangerForm passwordChangerForm = new PasswordChangerForm(m_user);
            passwordChangerForm.ShowDialog();
        }
    }
}