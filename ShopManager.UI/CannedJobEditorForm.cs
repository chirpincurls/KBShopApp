using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class CannedJobEditorForm : Form
    {
        public TextBox txtJobName;
        private DataGridView gridItems;
        private Button btnAddPart;
        private Button btnAddLabor;
        private Button btnRemove;
        private Button btnSave;
        private Button btnCancel;

        public string JobName => txtJobName.Text;
        public List<CannedJobItem> JobItems { get; private set; }

        public CannedJobEditorForm(string jobName = "", List<CannedJobItem> items = null)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(jobName))
            {
                txtJobName.Text = jobName;
                // If editing an existing job, we might want to lock the name or handle renaming logic.
                // For simplicity, we'll allow editing, but the caller handles the key update.
            }
            
            // Create a shallow copy of the list so we don't modify the original until Save is clicked
            JobItems = items != null ? new List<CannedJobItem>(items) : new List<CannedJobItem>();
            RefreshGrid();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Canned Job";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var lblName = new Label { Text = "Job Name:", Location = new Point(20, 20), AutoSize = true };
            txtJobName = new TextBox { Location = new Point(100, 17), Width = 300 };

            gridItems = new DataGridView();
            gridItems.Location = new Point(20, 60);
            gridItems.Size = new Size(540, 300);
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.RowHeadersVisible = false;
            gridItems.AllowUserToAddRows = false;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridItems.BackgroundColor = SystemColors.Window;
            
            gridItems.Columns.Add("Type", "Type");
            gridItems.Columns.Add("Description", "Description");
            gridItems.Columns.Add("Qty", "Qty");
            gridItems.Columns.Add("Price", "Price");

            btnAddLabor = new Button { Text = "Add Labor", Location = new Point(20, 370), Width = 100 };
            btnAddLabor.Click += BtnAddLabor_Click;

            btnAddPart = new Button { Text = "Add Part", Location = new Point(130, 370), Width = 100 };
            btnAddPart.Click += BtnAddPart_Click;

            btnRemove = new Button { Text = "Remove", Location = new Point(240, 370), Width = 100 };
            btnRemove.Click += BtnRemove_Click;

            btnSave = new Button { Text = "Save", DialogResult = DialogResult.OK, Location = new Point(380, 410), Width = 90 };
            
            btnCancel = new Button { Text = "Cancel", DialogResult = DialogResult.Cancel, Location = new Point(480, 410), Width = 90 };

            this.Controls.AddRange(new Control[] { lblName, txtJobName, gridItems, btnAddLabor, btnAddPart, btnRemove, btnSave, btnCancel });
        }

        private void RefreshGrid()
        {
            gridItems.Rows.Clear();
            foreach (var item in JobItems)
            {
                gridItems.Rows.Add(
                    item.IsLabor ? "Labor" : "Part",
                    item.Description,
                    item.Quantity,
                    item.Price
                );
            }
        }

        private void BtnAddLabor_Click(object sender, EventArgs e)
        {
            var form = new LaborForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                decimal.TryParse(form.txtHours.Text, out decimal qty);
                decimal.TryParse(form.txtTotal.Text, out decimal total);
                
                JobItems.Add(new CannedJobItem { IsLabor = true, Description = form.txtWorkPerformed.Text, Quantity = qty, Price = total });
                RefreshGrid();
            }
        }

        private void BtnAddPart_Click(object sender, EventArgs e)
        {
            var form = new PartsForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                decimal.TryParse(form.txtQuantity.Text, out decimal qty);
                decimal.TryParse(form.txtUnitRetail.Text, out decimal price);
                decimal.TryParse(form.txtUnitCost.Text, out decimal cost);

                JobItems.Add(new CannedJobItem { IsLabor = false, PartNo = form.txtPartNo.Text, Description = form.txtDescription.Text, Quantity = qty, Price = price, Cost = cost });
                RefreshGrid();
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (gridItems.SelectedRows.Count > 0 && gridItems.SelectedRows[0].Index < JobItems.Count)
            {
                JobItems.RemoveAt(gridItems.SelectedRows[0].Index);
                RefreshGrid();
            }
        }
    }
}