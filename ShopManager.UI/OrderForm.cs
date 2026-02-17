using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopManager.UI
{
    public class OrderForm : Form
    {
        private DataGridView gridItems;
        private Label lblPartsTotalVal;
        private Label lblLaborTotalVal;
        private Label lblTaxVal;
        private Label lblGrandTotalVal;
        private ContextMenuStrip ctxMenu;

        public OrderForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Order Form";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // --- 1. Top Sub Menu Buttons ---
            var pnlMenu = new FlowLayoutPanel();
            pnlMenu.Dock = DockStyle.Top;
            pnlMenu.Height = 60;
            pnlMenu.Padding = new Padding(10);
            pnlMenu.BackColor = SystemColors.ControlLight;

            var btnLabor = CreateMenuButton("Labor");
            btnLabor.Click += (s, e) => 
            {
                var form = new LaborForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    gridItems.Rows.Add(
                        form.txtHours.Text,
                        form.txtWorkPerformed.Text,
                        form.txtHours.Text,
                        "", 
                        form.txtTotal.Text,
                        "", 
                        "LABOR",
                        "0.00",
                        form.txtTotal.Text
                    );
                    UpdateTotals();
                }
            };

            var btnParts = CreateMenuButton("Parts");
            btnParts.Click += (s, e) => 
            {
                var form = new PartsForm();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    decimal.TryParse(form.txtQuantity.Text, out decimal qty);
                    decimal.TryParse(form.txtUnitRetail.Text, out decimal price);
                    decimal total = qty * price;

                    gridItems.Rows.Add(
                        "",
                        form.txtDescription.Text,
                        form.txtQuantity.Text,
                        form.txtUnitRetail.Text,
                        total.ToString("F2"),
                        "",
                        form.txtPartNo.Text,
                        form.txtUnitCost.Text,
                        total.ToString("F2")
                    );
                    UpdateTotals();
                }
            };

            var btnNotes = CreateMenuButton("Notes");
            var btnCanned = CreateMenuButton("Canned Jobs");
            btnCanned.Click += (s, e) => 
            {
                var form = new CannedJobsForm();
                if (form.ShowDialog() == DialogResult.OK && form.SelectedItems != null)
                {
                    foreach (var item in form.SelectedItems)
                    {
                        decimal total = item.Quantity * item.Price;
                        
                        if (item.IsLabor)
                        {
                            gridItems.Rows.Add(
                                item.Quantity.ToString("F1"),
                                item.Description,
                                item.Quantity.ToString("F1"),
                                "", 
                                total.ToString("F2"),
                                "", 
                                "LABOR",
                                "0.00",
                                total.ToString("F2")
                            );
                        }
                        else
                        {
                            gridItems.Rows.Add(
                                "",
                                item.Description,
                                item.Quantity.ToString("F1"),
                                item.Price.ToString("F2"),
                                total.ToString("F2"),
                                "",
                                item.PartNo,
                                item.Cost.ToString("F2"),
                                total.ToString("F2")
                            );
                        }
                    }
                    UpdateTotals();
                }
            };

            pnlMenu.Controls.AddRange(new Control[] { btnLabor, btnParts, btnNotes, btnCanned });

            // --- 2. Bottom Totals Section ---
            var pnlBottom = new Panel();
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Height = 120;
            pnlBottom.BackColor = SystemColors.ControlLight;

            // Layout for totals (Aligned to the right)
            int labelX = 850;
            int valueX = 950;
            int startY = 15;
            int stepY = 25;

            var lblParts = new Label { Text = "Parts:", Location = new Point(labelX, startY), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblPartsTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY), AutoSize = true };

            var lblLabor = new Label { Text = "Labor:", Location = new Point(labelX, startY + stepY), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblLaborTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY), AutoSize = true };

            var lblTax = new Label { Text = "Tax:", Location = new Point(labelX, startY + stepY * 2), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblTaxVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY * 2), AutoSize = true };

            var lblTotal = new Label { Text = "Total:", Location = new Point(labelX, startY + stepY * 3), AutoSize = true, Font = new Font(this.Font, FontStyle.Bold) };
            lblGrandTotalVal = new Label { Text = "$0.00", Location = new Point(valueX, startY + stepY * 3), AutoSize = true };

            pnlBottom.Controls.AddRange(new Control[] { 
                lblParts, lblPartsTotalVal,
                lblLabor, lblLaborTotalVal,
                lblTax, lblTaxVal,
                lblTotal, lblGrandTotalVal
            });

            // --- 3. Grid Section ---
            gridItems = new DataGridView();
            gridItems.Dock = DockStyle.Fill;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.BackgroundColor = SystemColors.Window;
            gridItems.BorderStyle = BorderStyle.None;
            gridItems.RowHeadersVisible = false;
            gridItems.AllowUserToAddRows = true;
            gridItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // Context Menu & Events
            ctxMenu = new ContextMenuStrip();
            var btnDeleteRow = new ToolStripMenuItem("Delete");
            btnDeleteRow.Click += (s, e) => DeleteSelectedRows();
            ctxMenu.Items.Add(btnDeleteRow);
            gridItems.ContextMenuStrip = ctxMenu;

            gridItems.KeyDown += (s, e) => { if (e.KeyCode == Keys.Delete) DeleteSelectedRows(); };
            gridItems.CellDoubleClick += (s, e) => { if (e.RowIndex >= 0) EditRow(gridItems.Rows[e.RowIndex]); };

            // Columns: Time, Description, Quantity, Sale, Extended, Rate, Part No, Cost, Price
            gridItems.Columns.Add("Time", "Time");
            gridItems.Columns.Add("Description", "Description");
            gridItems.Columns.Add("Quantity", "Quantity");
            gridItems.Columns.Add("Sale", "Sale");
            gridItems.Columns.Add("Extended", "Extended");
            gridItems.Columns.Add("Rate", "Rate");
            gridItems.Columns.Add("PartNo", "Part No.");
            gridItems.Columns.Add("Cost", "Cost");
            gridItems.Columns.Add("Price", "Price");

            // Add Controls (Dock order: Fill first, then Top/Bottom)
            this.Controls.Add(gridItems);
            this.Controls.Add(pnlMenu);
            this.Controls.Add(pnlBottom);
        }

        private Button CreateMenuButton(string text)
        {
            return new Button 
            { 
                Text = text, 
                Width = 120, 
                Height = 40, 
                Margin = new Padding(5) 
            };
        }

        private void UpdateTotals()
        {
            decimal parts = 0;
            decimal labor = 0;

            foreach (DataGridViewRow row in gridItems.Rows)
            {
                if (row.IsNewRow) continue;

                var partNo = row.Cells["PartNo"].Value?.ToString();
                decimal.TryParse(row.Cells["Extended"].Value?.ToString(), out decimal amount);

                if (partNo == "LABOR")
                    labor += amount;
                else
                    parts += amount;
            }

            lblPartsTotalVal.Text = parts.ToString("C");
            lblLaborTotalVal.Text = labor.ToString("C");
            lblGrandTotalVal.Text = (parts + labor).ToString("C");
        }

        private void DeleteSelectedRows()
        {
            if (gridItems.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in gridItems.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        gridItems.Rows.Remove(row);
                    }
                }
                UpdateTotals();
            }
        }

        private void EditRow(DataGridViewRow row)
        {
            if (row.IsNewRow) return;

            string partNo = row.Cells["PartNo"].Value?.ToString();

            if (partNo == "LABOR")
            {
                var form = new LaborForm();
                form.txtHours.Text = row.Cells["Time"].Value?.ToString();
                form.txtWorkPerformed.Text = row.Cells["Description"].Value?.ToString();
                form.txtTotal.Text = row.Cells["Extended"].Value?.ToString();
                
                if (form.ShowDialog() == DialogResult.OK)
                {
                    row.Cells["Time"].Value = form.txtHours.Text;
                    row.Cells["Description"].Value = form.txtWorkPerformed.Text;
                    row.Cells["Quantity"].Value = form.txtHours.Text;
                    row.Cells["Extended"].Value = form.txtTotal.Text;
                    row.Cells["Price"].Value = form.txtTotal.Text;
                    UpdateTotals();
                }
            }
            else
            {
                var form = new PartsForm();
                form.txtPartNo.Text = row.Cells["PartNo"].Value?.ToString();
                form.txtDescription.Text = row.Cells["Description"].Value?.ToString();
                form.txtQuantity.Text = row.Cells["Quantity"].Value?.ToString();
                form.txtUnitRetail.Text = row.Cells["Sale"].Value?.ToString();
                form.txtUnitCost.Text = row.Cells["Cost"].Value?.ToString();

                if (form.ShowDialog() == DialogResult.OK)
                {
                    decimal.TryParse(form.txtQuantity.Text, out decimal qty);
                    decimal.TryParse(form.txtUnitRetail.Text, out decimal price);
                    decimal total = qty * price;

                    row.Cells["PartNo"].Value = form.txtPartNo.Text;
                    row.Cells["Description"].Value = form.txtDescription.Text;
                    row.Cells["Quantity"].Value = form.txtQuantity.Text;
                    row.Cells["Sale"].Value = form.txtUnitRetail.Text;
                    row.Cells["Cost"].Value = form.txtUnitCost.Text;
                    row.Cells["Extended"].Value = total.ToString("F2");
                    row.Cells["Price"].Value = total.ToString("F2");
                    UpdateTotals();
                }
            }
        }
    }
}