using System.Windows.Forms;
using InstaInvLibrary;

namespace InstaInvUI
{
    public partial class Form1 : Form
    {
        private List<InvoiceItem> invoiceItems = new List<InvoiceItem>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string itemName = textItemName.Text;
            string itemPriceText = textItemPrice.Text.Trim();
            string itemQuantityText = textItemQuantity.Text.Trim();

            if (string.IsNullOrEmpty(itemName) || string.IsNullOrEmpty(itemPriceText) || string.IsNullOrEmpty(itemQuantityText))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(itemPriceText, out decimal itemPrice) || !int.TryParse(itemQuantityText, out int itemQuantity))
            {
                MessageBox.Show("Invalid price or quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            invoiceItems.Add(new InvoiceItem { Name = itemName, Price = itemPrice, Quantity = itemQuantity });

            UpdateListView();
            ClearInputFields();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Remove item button
            foreach (ListViewItem selectedItem in listView1.SelectedItems)
            {
                int selectedIndex = selectedItem.Index;
                invoiceItems.RemoveAt(selectedIndex);
                listView1.Items.Remove(selectedItem);
            }
        }

        private void btnGenerateInv_Click(object sender, EventArgs e)
        {
            string directoryPath = @"D:\source\repos\InstaInv\Invoices\";

            string fileName = $"Invoice_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string filePath = Path.Combine(directoryPath, fileName);

            try
            {
                if (invoiceItems.Count == 0)
                {
                    MessageBox.Show("No items to generate invoice.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                InvoiceGenerator.GeneratePdf(filePath, invoiceItems);
                MessageBox.Show("Invoice generated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void UpdateListView()
        {
            // Update ListView with invoice items
            listView1.Items.Clear();
            foreach (InvoiceItem item in invoiceItems)
            {
                ListViewItem listViewItem = new ListViewItem(new[] { item.Name, item.Price.ToString(), item.Quantity.ToString() });
                listView1.Items.Add(listViewItem);
            }
        }

        private void ClearInputFields()
        {
            // Clear input fields
            textItemName.Clear();
            textItemPrice.Clear();
            textItemQuantity.Clear();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by Z.Y. Sun");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
