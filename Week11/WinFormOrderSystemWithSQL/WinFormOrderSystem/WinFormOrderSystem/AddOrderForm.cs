using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormOrderSystem
{
    public partial class AddOrderForm : Form
    {
        private AddItemForm addItemForm;
        public AddOrderForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (addItemForm == null || addItemForm.IsDisposed)
                addItemForm = new AddItemForm();
            else ;
            addItemForm.ShowDialog();
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
