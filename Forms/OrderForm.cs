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
using System.Data.Entity;
namespace project_addition_app.Forms
{
    public partial class OrderForm : Form
    {
        AdisyonDBEntities context = new AdisyonDBEntities();
        Table m_table;
        Order m_order;
        public OrderForm(Table table, Order order = null)
        {
            InitializeComponent();

            m_table = table;
            if (order == null)
            {
                m_order = new Order();
                m_table.Durum = true;
                m_order.Durum = true;
                m_order.ToplamTutar = 0;
                m_order.Tarih = DateTime.Now;
                m_order.TableId = table.Id;
                context.Orders.Add(m_order);
                context.SaveChanges();
            }
            else
            {
                m_order = order;
            }
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadData();
            if (tabControl1.TabPages.Count > 0)
            {
                tabControl1.TabPages.RemoveAt(0); // İlk TabPage'i kaldır
            }
            foreach (TabPage tab in tabControl1.TabPages)
            {
                var panel = tab.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                if (panel != null)
                {
                    ResizeButtons(panel); // Varsayılan olarak 4 buton yan yana
                }
            }
        }

        public void LoadProducts()
        {
            //953; 623 6;12
            foreach (var category in context.Categories.ToList())
            {
                // Yeni bir TabPage oluştur
                TabPage tabPage = new TabPage(category.KategoriAdi);

                // Her TabPage için bir FlowLayoutPanel oluştur
                FlowLayoutPanel panel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = Color.SaddleBrown,
                };
                // TabPage içine FlowLayoutPanel ekle
                tabPage.Controls.Add(panel);

                // TabPage'i TabControl'e ekle
                tabControl1.TabPages.Add(tabPage);

                // İlgili kategorideki ürünleri getir
                var products = context.Products
                                      .Where(p => p.CategoryId == category.Id)
                                      .ToList();
                //var products = context.Products.ToList();

                foreach (var product in products)
                {
                    Button btn = new Button();
                    btn.Text = product.UrunAdi;
                    btn.Tag = product;
                    btn.BackColor = Color.FromArgb(0xFF, 0x4B, 0x36, 0x21);
                    btn.ForeColor = Color.Gold;
                    btn.Click += (s, e) =>
                    {
                        OrderDetail detail = new OrderDetail();
                        detail.ProductId = product.Id;
                        detail.OrderId = m_order.Id;
                        detail.BirimFiyat = product.Fiyat;
                        detail.ToplamFiyat = product.Fiyat;
                        detail.UrunAdi = product.UrunAdi;
                        detail.Adet = 1;
                        context.OrderDetails.Add(detail);
                        var orderInContext = context.Orders.First(x => x.Id == m_order.Id);
                        orderInContext.ToplamTutar += detail.ToplamFiyat;
                        context.SaveChanges();
                        LoadData();
                        ResizeButtons(panel);

                    };
                    panel.Controls.Add(btn);
                }
            }
        }

        public void LoadData()
        {
            var details = context.SiparisDetaylari_VV.Where(x => x.Sipariş_Numarası == m_order.Id).ToList();
            order_detailsDGV.DataSource = details;

        }
        private void OrderForm_Resize(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                var panel = tab.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                if (panel != null)
                {
                    ResizeButtons(panel); // Varsayılan olarak 4 buton yan yana
                }
            }

        }
        public void ResizeButtons(FlowLayoutPanel flowLayoutPanel1)
        {
            int butonSayisiYanYana = 4; // Varsayılan olarak yan yana kaç buton olsun istiyorsan
            int padding = 10;

            // Her butonun genişliği = Panelin genişliği / buton sayısı - boşluk
            int butonGenislik = (flowLayoutPanel1.Width / butonSayisiYanYana) - padding;

            foreach (Control ctrl in flowLayoutPanel1.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.Width = butonGenislik;
                    btn.Height = (int)(butonGenislik * 0.8); // Yüksekliği orantılı istersen
                    btn.BackColor = Color.FromArgb(0xFF, 0x4B, 0x36, 0x21);
                    btn.ForeColor = Color.Gold;
                }
            }
        }

        private void order_detailsDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                var rowId = order_detailsDGV.Rows[e.RowIndex].Cells["SiparisDetayiId"].Value;
                if (e.ColumnIndex == 0) {
                    var detail = context.OrderDetails.Find(rowId);
                    var orderInContext = context.Orders.First(x => x.Id == m_order.Id);
                    orderInContext.ToplamTutar -= detail.ToplamFiyat;
                    context.OrderDetails.Remove(detail);
                    context.SaveChanges();
                    LoadData();
                    foreach (TabPage tab in tabControl1.TabPages)
                    {
                        var panel = tab.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                        if (panel != null)
                        {
                            ResizeButtons(panel); // Varsayılan olarak 4 buton yan yana
                        }
                    }
                }
            
            }
        }
    }
}
