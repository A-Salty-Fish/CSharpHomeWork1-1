using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace WinFormOrderSystem
{
    public partial class SearchForm : Form
    {
        public event Action<string> ByNameEvent;
        public event Action<long> ByNumEvent;
        public event Action<int> ByWhichEvent;

        public SearchForm()
        {
            InitializeComponent();
            tabControl1.TabPages[0].Text = "订单号查询";
            tabControl1.TabPages[1].Text = "用户名查询";

            SearchGroupBox.Location = new Point(this.Width * 12 / 450, this.Height * 12 / 300);
            SearchGroupBox.Width = this.Width * 404 / 450;
            SearchGroupBox.Height = this.Height * 177 / 300;

            tabControl1.Location = new Point(this.Width * 6 / 450, this.Height * 27 / 300);
            tabControl1.Width = this.Width * 392 / 450;
            tabControl1.Height = this.Height * 144 / 300;

            SearchButton.Location = new Point(this.Width * 125 / 450, this.Height * 194 / 300);
            SearchButton.Width = this.Width * 120 / 450;
            SearchButton.Height = this.Height * 38 / 300;

        }

        private void SearchForm_Resize(object sender, EventArgs e)
        {
            SearchGroupBox.Location = new Point(this.Width * 12 / 450, this.Height * 12 / 300);
            SearchGroupBox.Width = this.Width * 404 / 450;
            SearchGroupBox.Height = this.Height * 177 / 300;

            tabControl1.Location = new Point(this.Width * 6 / 450, this.Height * 27 / 300);
            tabControl1.Width = this.Width * 392 / 450;
            tabControl1.Height = this.Height * 144 / 300;

            SearchButton.Location = new Point(this.Width * 156 / 450, this.Height * 194 / 300);
            SearchButton.Width = this.Width * 120 / 450;
            SearchButton.Height = this.Height * 38 / 300;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            ByWhichEvent(tabControl1.SelectedIndex);
            if (tabControl1.SelectedIndex == 0)
            {
                ByNumEvent(long.Parse(OrderNumTextBox.Text));
                ByNameEvent(null);
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                ByNumEvent(-1);
                ByNameEvent(OrderNameTextBox.Text);
            }
            else return;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ByWhichEvent(tabControl1.SelectedIndex);
        }
    }
}
