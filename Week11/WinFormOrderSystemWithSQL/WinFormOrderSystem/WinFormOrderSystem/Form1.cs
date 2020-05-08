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
using System.Xml.XPath;
using OrderManageSystem;

namespace WinFormOrderSystem
{
    public partial class Form1 : Form
    {
        public OrderService orderService;
        public List<OrderItem> ItemSource;
        public AlgorithmUnit algorithmUnit;

        public int which;
        public string customername;
        public long num;

        public Order AddOrder;

        private SearchForm searchForm;
        private AddOrderForm addOrderForm;

        public Form1()
        {
            orderService = new OrderService();
            ItemSource = new List<OrderItem>();
            algorithmUnit=new AlgorithmUnit();

            which = -1;
            num = -1;

            InitializeComponent();

            ResultText.Location = new Point(this.Width * 12 / 960, this.Height * 357 / 540);
            ResultText.Height = this.Height * 115 / 540;
            ResultText.Width = this.Width * 783 / 960;

            OrderDataGridView.Location= new Point(this.Width * 13 / 960, this.Height * 45 / 540);
            OrderDataGridView.Height = this.Height * 135 / 540;
            OrderDataGridView.Width = this.Width * 782 / 960;

            ItemDataGridView.Location = new Point(this.Width * 13 / 960, this.Height * 216 / 540);
            ItemDataGridView.Height = this.Height * 135 / 540;
            ItemDataGridView.Width = this.Width * 782 / 960;

            flowLayoutPanel1.Location = new Point(this.Width * 803 / 960, this.Height * 16 / 540);
            flowLayoutPanel1.Height = this.Height * 456 / 540;
            flowLayoutPanel1.Width = this.Width * 121 / 960;

            ImportButton.Width = ExportButton.Width = AddButton.Width = ModifyButton.Width
                = DeleteButton.Width = SearchButton.Width = SortButton.Width = ShowAllButton.Width
                    = this.Width * 112 / 960;
            ImportButton.Height = ExportButton.Height = AddButton.Height = ModifyButton.Height
                = DeleteButton.Height = SearchButton.Height = SortButton.Height = ShowAllButton.Height
                    = this.Height * 51 / 540;
            //ImportButton.Font = new Font("宋体", 30F * (this.Width * this.Height) / (960 * 540));
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\C sharp\\CSharpHomeWork1-1\\Week8\\WinFormOrderSystem\\WinFormOrderSystem\\WinFormOrderSystem\\bin\\Debug";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    fileName = openFileDialog.FileName;
                    orderService.Import(fileName);
                    OrderDataGridView.DataSource = orderService.orderList;
                    ItemDataGridView.DataSource = ItemSource;
                }
            }
        }
        private void ExportButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "E:\\C sharp\\CSharpHomeWork1-1\\Week8\\WinFormOrderSystem\\WinFormOrderSystem\\WinFormOrderSystem\\bin\\Debug";
                openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    fileName = openFileDialog.FileName;
                    orderService.Export(fileName);
                }
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            ResultText.Location = new Point(this.Width * 12 / 960, this.Height * 357 / 540);
            ResultText.Height = this.Height * 115 / 540;
            ResultText.Width = this.Width * 783 / 960;

            OrderDataGridView.Location = new Point(this.Width * 13 / 960, this.Height * 45 / 540);
            OrderDataGridView.Height = this.Height * 135 / 540;
            OrderDataGridView.Width = this.Width * 782 / 960;

            ItemDataGridView.Location = new Point(this.Width * 12 / 960, this.Height * 216 / 540);
            ItemDataGridView.Height = this.Height * 135 / 540;
            ItemDataGridView.Width = this.Width * 782 / 960;

            flowLayoutPanel1.Location=new Point(this.Width * 803 / 960, this.Height * 16 / 540);
            flowLayoutPanel1.Height = this.Height * 456 / 540;
            flowLayoutPanel1.Width = this.Width * 121 / 960;

            ImportButton.Width = ExportButton.Width = AddButton.Width = ModifyButton.Width
                = DeleteButton.Width = SearchButton.Width = SortButton.Width = ShowAllButton.Width
                    = this.Width * 112 / 960;

            ImportButton.Height = ExportButton.Height = AddButton.Height = ModifyButton.Height
                = DeleteButton.Height = SearchButton.Height = SortButton.Height = ShowAllButton.Height
                    = this.Height * 51 / 540;
        }

        private void ShowAllButton_Click(object sender, EventArgs e)
        {
            ItemSource.Clear();
            foreach (var x in orderService.orderList)
            {
                ItemSource = ItemSource.Concat(x.orderItemsList).ToList();
            }

            ItemDataGridView.DataSource = ItemSource;
            OrderDataGridView.DataSource = orderService.orderList;
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (searchForm == null || searchForm.IsDisposed)
            {
                searchForm = new SearchForm();
            }
            else ;
                searchForm.ByWhichEvent += n => which = n;
                searchForm.ByNumEvent += n => num = n;
                searchForm.ByNameEvent += n => customername = n;
                searchForm.ShowDialog();
                if (which == 0)
                {
                    List<Order> resultList=new List<Order>();
                    resultList.Add(algorithmUnit.SearchByNum(num, orderService.orderList));
                    OrderDataGridView.DataSource = resultList;
                    ItemDataGridView.DataSource = resultList[0].orderItemsList;
                }
                else if (which == 1)
                {
                    List<OrderItem> ItemResult=new List<OrderItem>();
                    List<Order> OrderResult = algorithmUnit.SearchByName(customername, orderService.orderList).ToList();
                    foreach (var x in OrderResult)
                    {
                        ItemResult = ItemResult.Concat(x.orderItemsList).ToList();
                    }

                    OrderDataGridView.DataSource = OrderResult;
                    ItemDataGridView.DataSource = ItemResult;
                }
                else ;
        }

        private void SortButton_Click(object sender, EventArgs e)
        {
            algorithmUnit.Sort(SortWay.BySum,orderService.orderList);
            OrderDataGridView.DataSource = orderService.orderList;
            ItemSource.Clear();
            foreach (var x in orderService.orderList)
            {
                ItemSource = ItemSource.Concat(x.orderItemsList).ToList();
            }

            ItemDataGridView.DataSource = ItemSource;
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            AddOrder = new Order();
            if (addOrderForm == null || addOrderForm.IsDisposed)
                addOrderForm = new AddOrderForm();
            else ;
            addOrderForm.ShowDialog();
        }

        private void OrderDataGridView_Click(object sender, EventArgs e)
        {
            int index = OrderDataGridView.CurrentCell.RowIndex;
            if (index >= orderService.orderList.Count) ;
            else
            {
                Order currentOrder = orderService.orderList[index];
                if (currentOrder == null) ;
                else
                {
                    ItemSource = currentOrder.orderItemsList;
                    ItemDataGridView.DataSource = ItemSource;
                }
            }
        }

        private void OrderDataGridView_DoubleClick(object sender, EventArgs e)
        {
            //Order addOrder = new Order();
            //orderService.orderList.Add(addOrder);
            OrderDataGridView.DataSource = orderService.orderList;
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {

        }
    }
}
