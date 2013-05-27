using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewADVMaker
{
    public partial class frmRequestor : Form
    {
        public string selectedString { get; set; }

        public frmRequestor()
        {
            InitializeComponent();
        }

        public void AddToList(string itemToAdd)
        {
            lbItems.Items.Add(itemToAdd);
        }
        public void ClearList()
        {
            lbItems.Items.Clear();
        }

        private void lbItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbItems.SelectedItems.Count > 0)
            {
                selectedString = lbItems.SelectedItems[0].ToString();
                Console.WriteLine(selectedString);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Hide();
        }
    }
}
